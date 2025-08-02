using Infrastructure.Extensions;
using Infrastructure.Interfaces.Save.EfCore;
using Infrastructure.ObjectPool;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Shared;

public abstract class EfCoreHandlerBase<TContext, T> : Database<TContext, T>, IDataHandlerEfCore 
    where T : class where TContext : DbContext
{
    public virtual string Name => nameof(T);
    
    protected readonly IServiceProvider _serviceProvider;
    protected readonly ILogger<TContext> _logger;
    
    protected EfCoreHandlerBase(IServiceProvider serviceProvider, ILogger<TContext> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task ProcessAsync(CancellationToken token)
    {
        var buffer = new List<T>();
        var delay = Task.Delay(TimeSpan.FromSeconds(2), token);
        
        using var scope = _serviceProvider.CreateScope();
        
        using var context = scope.ServiceProvider.GetRequiredService<TContext>();

        while (!token.IsCancellationRequested)
        {
            if (buffer.Count == 0)
                await Reader.WaitToReadAsync(token);
            
            try
            {
                var readTask = Reader.ReadAsync().AsTask();
                var completed = await Task.WhenAny(readTask, delay);

                if (completed == delay && buffer.Count > 0 || buffer.Count == 3000)
                {
                    await context.BulkSaveWithTransactionAsync(buffer, token);
                    buffer.Clear();
                    
                    continue;
                }
                
                buffer.Add(await readTask);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
        }
    }
}
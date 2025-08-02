using Infrastructure.Interfaces.Save.EfCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore;

public class EfCoreDbSyncService : BackgroundService
{
    private readonly ILogger<EfCoreDbSyncService> _logger;
    private readonly IEnumerable<IDataHandlerEfCore>  _handlers;

    public EfCoreDbSyncService(IEnumerable<IDataHandlerEfCore> handlers)
    {
        _handlers = handlers;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task>();
        
        foreach (var handler in _handlers)
            tasks.Add(Task.Run(() => handler.ProcessAsync(stoppingToken), stoppingToken));

        await Task.WhenAll(tasks);
    }
}
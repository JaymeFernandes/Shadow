using System.Collections.Concurrent;
using System.Threading.Channels;
using Infrastructure.Contexts.Mongo.Shared;
using Infrastructure.Extensions;
using Infrastructure.Interfaces.Save.Mongo;
using Infrastructure.ObjectPool;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Shared;

public abstract class MongoBaseHandler<TContext, T> : Database<TContext, T>, IDataHandlerMongo where T : class where TContext : DbMongo
{
    protected abstract Func<T, FilterDefinition<T>> Filter { get; }
    public virtual string Name => nameof(T);

    private readonly ILogger<IMongoCollection<T>> _logger;
    
    protected abstract IMongoCollection<T> Collection { get; }

    protected MongoBaseHandler(ILogger<IMongoCollection<T>> logger, TContext context)
    {
        Context = context;
        _logger = logger;
    }


    public virtual async Task ProcessAsync(CancellationToken token)
    {
        var buffer = new List<T>();
        var delay = Task.Delay(TimeSpan.FromSeconds(2), token);
        
        while (!token.IsCancellationRequested)
        {
            if (buffer.Count == 0)
                await Reader.WaitToReadAsync(token);
                
            var readTask = Reader.ReadAsync().AsTask();
            var completed = await Task.WhenAny(readTask, delay);

            if (completed == delay && buffer.Count > 0 || buffer.Count == 3000)
            {
                    
                if (await SaveAsync(buffer, token))
                {
                    if(await SaveAsync(buffer, token))
                        buffer.Clear();
                        
                    continue;
                }
            }
                
            var item = await readTask;
            if(!buffer.Contains(item))
                buffer.Add(item);
        }
    }

    private async Task<bool> SaveAsync(List<T> buffer, CancellationToken token)
    {
        try
        {
            var bulkOpsGroups = GetBulkOpsGroups(buffer, Filter);

            foreach (var batch in bulkOpsGroups)
                await Collection.BulkWriteAsync(batch, cancellationToken: token);
        
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    
    private IEnumerable<IEnumerable<WriteModel<T>>> GetBulkOpsGroups<T>(List<T> values, Func<T, FilterDefinition<T>> filterFunction) where T : class
    {
        return values
            .Select((x, i) =>
            {
                var filter = filterFunction(x);

                return new ReplaceOneModel<T>(filter, x)
                {
                    IsUpsert = true
                };
            })
            .Select((operation, index) => new { operation, index })
            .GroupBy(x => x.index / 1000)
            .Select(g => g.Select(x => x.operation));
    }
}
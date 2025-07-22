using System.Collections.Concurrent;
using Infrastructure.Interfaces.Save.Mongo;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Shared;

public abstract class MongoBaseHandler<T> : IDataHandlerMongo where T : class
{
    protected abstract ConcurrentQueue<T> Queue { get; }
    protected abstract IMongoCollection<T> Collection { get; }
    
    protected abstract Func<T, FilterDefinition<T>> Filter { get; }
    
    public virtual string Name => nameof(T);
    public virtual bool HasPendingItems => !Queue.IsEmpty;

    
    public async Task ProcessAsync(CancellationToken token)
    {
        List<T> queues = new();

        while (Queue.TryDequeue(out var post))
            queues.Add(post);

        var bulkOpsGroups = GetBulkOpsGroups(queues, Filter);

        foreach (var batch in bulkOpsGroups)
            await Collection.BulkWriteAsync(batch, cancellationToken:token);
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
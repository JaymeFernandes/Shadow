using Infrastructure.Interfaces.Save.Mongo;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.Mongo;

public class MongoDbSyncService : BackgroundService
{
    private IEnumerable<IDataHandlerMongo> _handlers;
    private ILogger<IDataHandlerMongo> _logger;

    public MongoDbSyncService(IEnumerable<IDataHandlerMongo> handlers, ILogger<IDataHandlerMongo> logger)
    {
        _handlers = handlers;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task>();
        
        foreach (var handler in _handlers)
            tasks.Add(Task.Run(() => handler.ProcessAsync(stoppingToken), stoppingToken));

        await Task.WhenAll(tasks);
    }
}
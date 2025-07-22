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
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var handler in _handlers)
            {
                if (handler.HasPendingItems)
                    try
                    {
                        await handler.ProcessAsync(stoppingToken);
                    }
                    catch
                    {
                        _logger.LogError("Erro ao salvar no Mongo Db {Handler}", handler.Name);
                    }
            }

            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
        }
    }
}
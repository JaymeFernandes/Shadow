

using Domain.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Contexts.Mongo.Shared;

public abstract class DbMongo
{
    private readonly IMongoDatabase _database;

    protected IMongoDatabase Database 
        => _database;

    public DbMongo(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.Connection);

        _database = client.GetDatabase(settings.Value.Database);

        this.OnConfigure();
    }

    public IMongoCollection<T> GetCollection<T>(string name)
        => _database.GetCollection<T>(name);

    protected abstract void OnConfigure();
}
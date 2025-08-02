using Domain.Models.User;
using Infrastructure.Contexts.Mongo.Shared;
using MongoDB.Driver;


namespace Infrastructure.Mappings.User;

public class UserMap : IMongoEntityConfiguration<MongoUser>
{
    public void Configure(IMongoCollection<MongoUser> collection)
    {
        var indexUserModels = new List<CreateIndexModel<MongoUser>>
        {
            new CreateIndexModel<MongoUser>(
                Builders<MongoUser>.IndexKeys.Ascending(u => u.Name),
                new CreateIndexOptions { Unique = true }),

            new CreateIndexModel<MongoUser>(
                Builders<MongoUser>.IndexKeys.Ascending(u => u.Code),
                new CreateIndexOptions { Unique = true }),

            new CreateIndexModel<MongoUser>(
                Builders<MongoUser>.IndexKeys.Ascending(u => u.Display)),

            new CreateIndexModel<MongoUser>(
                Builders<MongoUser>.IndexKeys.Descending(u => u.LastLoginAt)),

            new CreateIndexModel<MongoUser>(
                Builders<MongoUser>.IndexKeys.Ascending(u => u.CachedAt))
        };

        collection.Indexes.CreateMany(indexUserModels);
    }
}
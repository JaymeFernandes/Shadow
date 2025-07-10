
using Domain.Models.Post;
using Infrastructure.Contexts.Mongo.Shared;
using MongoDB.Driver;

namespace Infrastructure.Mappings.Post;

public class PostMap : IMongoEntityConfiguration<MongoPost>
{
    public void Configure(IMongoCollection<MongoPost> collection)
    {
        var indexPostModels = new List<CreateIndexModel<MongoPost>>
        {
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Ascending(p => p.Code)),
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Ascending(p => p.Author)),
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Ascending(p => p.Work)),
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Descending(p => p.CreatedAt)),
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Ascending(p => p.Lang)),
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Ascending("categories.code")),
            new CreateIndexModel<MongoPost>(Builders<MongoPost>.IndexKeys.Ascending("tags.code")),
        };

        collection.Indexes.CreateMany(indexPostModels);
    }
}
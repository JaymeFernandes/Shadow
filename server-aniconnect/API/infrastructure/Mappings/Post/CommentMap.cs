using Domain.Models.Post;
using Infrastructure.Contexts.Mongo.Shared;
using MongoDB.Driver;

namespace Infrastructure.Mappings.Post;

public class CommentMap : IMongoEntityConfiguration<MongoComment>
{
    public void Configure(IMongoCollection<MongoComment> collection)
    {
        var indexKeys = Builders<MongoComment>.IndexKeys;

        var indexs = new List<CreateIndexModel<MongoComment>>
        {
            new CreateIndexModel<MongoComment>(indexKeys.Ascending(x => x.PostId)),
            new CreateIndexModel<MongoComment>(indexKeys.Ascending(x => x.ParentCommentId)),
            new CreateIndexModel<MongoComment>(indexKeys.Ascending(x => x.PostId)
                .Descending(x => x.CreatedAt)),
        };

        collection.Indexes.CreateMany(indexs);
    }
}
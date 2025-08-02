using Domain.Models.Post;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Posts;

public class LikeHandler : MongoBaseHandler<MongoPostsContext, MongoLike>
{
    protected override Func<MongoLike, FilterDefinition<MongoLike>> Filter 
        => (x => Builders<MongoLike>.Filter.Eq(l => l.Id, x.Id));

    protected override IMongoCollection<MongoLike> Collection 
        => Context.Likes;

    public LikeHandler(ILogger<IMongoCollection<MongoLike>> logger, MongoPostsContext context) : base(logger, context)
    {
    }
}
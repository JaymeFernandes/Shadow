using Domain.Models.Post;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Posts;

public class PostsHandler : MongoBaseHandler<MongoPostsContext, MongoPost>
{
    protected override Func<MongoPost, FilterDefinition<MongoPost>> Filter
        => (x => Builders<MongoPost>.Filter.Eq(p => p.Id, x.Id));

    protected override IMongoCollection<MongoPost> Collection => Context.Posts;

    public PostsHandler(ILogger<IMongoCollection<MongoPost>> logger, MongoPostsContext context) : base(logger, context)
    {
    }
}
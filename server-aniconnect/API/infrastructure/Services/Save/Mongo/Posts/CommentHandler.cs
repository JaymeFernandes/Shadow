using Domain.Models.Post;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Posts;

public class CommentHandler : MongoBaseHandler<MongoPostsContext, MongoComment>
{
    protected override Func<MongoComment, FilterDefinition<MongoComment>> Filter
        => (x => Builders<MongoComment>.Filter.Eq(y => y.Id, x.Id));

    protected override IMongoCollection<MongoComment> Collection 
            => Context.Comments;

    public CommentHandler(ILogger<IMongoCollection<MongoComment>> logger, MongoPostsContext context) : base(logger, context)
    {
    }
}
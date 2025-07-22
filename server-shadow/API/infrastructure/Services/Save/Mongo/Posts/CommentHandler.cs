using System.Collections.Concurrent;
using Domain.Models.Post;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Posts;

public class CommentHandler : MongoBaseHandler<MongoComment>
{
    private readonly MongoPostsContext _context;
    private readonly MongoPool _pool;


    protected override ConcurrentQueue<MongoComment> Queue
        => _pool.CommentsQueue;

    protected override IMongoCollection<MongoComment> Collection
        => _context.Comments;
    
    protected override Func<MongoComment, FilterDefinition<MongoComment>> Filter
        => (x => Builders<MongoComment>.Filter.Eq(y => y.Id, x.Id));
    
    
    public CommentHandler(MongoPostsContext context, MongoPool pool)
    {
        _context = context;
        _pool = pool;
    }
}
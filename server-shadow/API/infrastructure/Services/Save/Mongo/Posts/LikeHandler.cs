using System.Collections.Concurrent;
using Domain.Models.Post;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Posts;

public class LikeHandler : MongoBaseHandler<MongoLike>
{
    private readonly MongoPostsContext _context;
    private readonly MongoPool _pool;

    protected override ConcurrentQueue<MongoLike> Queue
        => _pool.LikesQueue;

    protected override IMongoCollection<MongoLike> Collection
        => _context.Likes;
    
    protected override Func<MongoLike, FilterDefinition<MongoLike>> Filter 
        => (x => Builders<MongoLike>.Filter.Eq(l => l.Id, x.Id));
    
    
    public LikeHandler(MongoPostsContext context, MongoPool pool)
    {
        _context = context;
        _pool = pool;
    }
}
using System.Collections.Concurrent;
using Domain.Models.Post;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Posts;

public class PostsHandler : MongoBaseHandler<MongoPost>
{
    private readonly MongoPostsContext _context;
    private readonly MongoPool _pool;
    
    protected override ConcurrentQueue<MongoPost> Queue 
        => _pool.PostsQueue;
    
    protected override IMongoCollection<MongoPost> Collection 
        => _context.Posts;

    protected override Func<MongoPost, FilterDefinition<MongoPost>> Filter
        => (x => Builders<MongoPost>.Filter.Eq(p => p.Id, x.Id));
    

    public PostsHandler(MongoPostsContext context, MongoPool pool)
    {
        _context = context;
        _pool = pool;
    }
}
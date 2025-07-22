using System.Collections.Concurrent;
using Domain.Models.User;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Users;

public class UserHandler : MongoBaseHandler<MongoUser>
{
    private readonly MongoPool _pool;
    private readonly MongoUserContext _context;

    protected override ConcurrentQueue<MongoUser> Queue
        => _pool.UsersQueue;

    public override bool HasPendingItems 
        => !_pool.UsersQueue.IsEmpty;

    protected override IMongoCollection<MongoUser> Collection
        => _context.Users;
    protected override Func<MongoUser, FilterDefinition<MongoUser>> Filter 
        => (x => Builders<MongoUser>.Filter.Eq(u => u.Id, x.Id));


    public UserHandler(MongoPool pool, MongoUserContext context)
    {
        _pool = pool;
        _context = context;
    }
}
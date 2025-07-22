#nullable enable
using System.Security.Claims;
using Domain.Models.User;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace Api.Controllers.Profile.Me.Shared;

[Authorize]
[Tags("Profile")]
[ApiController]
[Route("api/users/me")]
public class ProfileControllerBase : ControllerBase
{
    private readonly MongoPool _mongoPool;
    private readonly MongoUserContext _context;
    private readonly IMemoryCache _cache;

    public ProfileControllerBase(MongoPool mongoPool, IMemoryCache cache, MongoUserContext context)
    {
        _mongoPool = mongoPool;
        _cache = cache;
        _context = context;
    }

    protected Task SaveUser(MongoUser user)
    {
        var tempCacheKey = $"user:temp:{user.Code}";
        
        var tempOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

        _cache.Set(tempCacheKey, user, tempOptions);
        
        return Task.CompletedTask;
    }
    
    protected async Task SaveUserAndUpdate(MongoUser user)
    {
        var primaryCacheKey = $"user:{user.Code}";

        var options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(5))
            .RegisterPostEvictionCallback(async (key, value, reason, state) =>
            {
                if (value is MongoUser expiredUser)
                {
                    _mongoPool.UsersQueue.Enqueue(expiredUser);

                    await SaveUser(expiredUser);
                }
            });
            
        _cache.Set(primaryCacheKey, user, options);
    }

    protected async Task<MongoUser?> GetUserAsync()
    {
        var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if(string.IsNullOrEmpty(id))
            return null;
        
        var key = $"user:{id}";
        var tempKey = $"user:temp:{id}";

        var filter = Builders<MongoUser>.Filter.Eq(x => x.Code, id);
            
        return _cache.TryGetValue<MongoUser>(key, out var value) ? value
            : _cache.TryGetValue<MongoUser>(tempKey, out value) ? value 
            : await _context.Users.Find(filter).FirstOrDefaultAsync();
    }
}
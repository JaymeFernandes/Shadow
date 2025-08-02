#nullable enable
using System.Security.Claims;
using Domain.Models.User;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace Api.Controllers.Profile.Me.Shared;

[Authorize]
[ApiController]
[Route("api/users/me")]
public class ProfileControllerBase : ControllerBase
{
    private readonly UserHandler _userHandler;
    private readonly MongoUserContext _context;
    private readonly IMemoryCache _cache;

    public ProfileControllerBase(IMemoryCache cache, MongoUserContext context, UserHandler userHandler)
    {
        _cache = cache;
        _context = context;
        _userHandler = userHandler;
    }

    protected Task SaveUser(MongoUser user)
    {
        var tempCacheKey = $"user:temp:{user.Code}";
        
        var tempOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

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
                    var tempCacheKey = $"user:temp:{user.Code}";
                    var tempOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(1));

                    _cache.Set(tempCacheKey, user, tempOptions);
                    
                    
                    
                    await _userHandler.Add(expiredUser);
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

        if (_cache.TryGetValue<MongoUser>(key, out var cache))
            return cache;

        if (_cache.TryGetValue<MongoUser>(tempKey, out var tempCache))
            return tempCache;
        
        return await _context.Users.Find(filter).FirstOrDefaultAsync();
    }
}
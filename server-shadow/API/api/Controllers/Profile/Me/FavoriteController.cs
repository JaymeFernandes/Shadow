using System.Security.Claims;
using Api.Controllers.Profile.Me.Shared;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers.Profile.Me;

public class FavoriteController : ProfileControllerBase
{
    private readonly MongoPool _mongoPool;
    private readonly IMemoryCache _memoryCache;
    
    public FavoriteController(MongoPool mongoPool, IMemoryCache cache, IMemoryCache memoryCache, MongoUserContext context) 
        : base(mongoPool, cache, context)
    {
        _mongoPool = mongoPool;
        _memoryCache = memoryCache;
    }

    [HttpGet("favorites")]
    public async Task<IActionResult> Get()
    {
        var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return Ok();
    }

    [HttpPost("favorites")]
    public async Task<IActionResult> Post()
    {
        var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return Ok();
    }

    [HttpDelete("favorites")]
    public async Task<IActionResult> Delete()
    {
        var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return Ok();
    }
}
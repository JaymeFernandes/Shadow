using System.Security.Claims;
using Api.Controllers.Profile.Me.Shared;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers.Profile.Me;

[Tags("Profile")]
public class FavoriteController : ProfileControllerBase
{
    private readonly IMemoryCache _memoryCache;

    public FavoriteController(IMemoryCache cache, MongoUserContext context, UserHandler userHandler, IMemoryCache memoryCache) : base(cache, context, userHandler)
    {
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
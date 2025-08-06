using Api.Controllers.Internal.Profile.Me.Shared;
using Api.Extensions;
using Application.DTOs;
using Application.DTOs.Profile;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers.Internal.Profile.Me;

[Tags("Profile")]
public class ProfileController : ProfileControllerBase
{
    private readonly MongoUserContext _context;
    private readonly IMemoryCache _cache;

    public ProfileController(IMemoryCache cache, MongoUserContext context, UserHandler userHandler) : base(cache, context, userHandler)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var user = await GetUserAsync();

        if (user == null)
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User not found"
            });

        return this.ApiResponseOk(data: user.ToDtoProfile());
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] PutProfileRequest request)
    {
        var user = await GetUserAsync();
            
        if (user == null)
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "User not found"
            });

        user.LastLoginAt = DateTime.UtcNow;
            
        user.Display = string.IsNullOrWhiteSpace(request.Display) ? 
            user.Display : request.Display;
            
        user.Lang = string.IsNullOrWhiteSpace(request.Lang) ?
            user.Lang : request.Lang;

        user.IsPrivate = request.IsPrivate;

        await SaveUserAndUpdate(user);
            
        return this.ApiResponseOk(data: user.ToDtoProfile());
    }
}
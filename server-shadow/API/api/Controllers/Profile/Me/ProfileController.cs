using System.Security.Claims;
using Api.Controllers.Profile.Me.Shared;
using Api.Extensions;
using Application.DTOs;
using Application.DTOs.Profile;
using Domain.Models.User;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace Api.Controllers.Profile.Me
{
    
    public class ProfileController : ProfileControllerBase
    {
        private readonly MongoUserContext _context;
        private readonly IMemoryCache _cache;


        public ProfileController(MongoPool mongoPool, IMemoryCache cache, MongoUserContext context) : base(mongoPool, cache, context)
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
        public async Task<IActionResult> Put([FromBody] ProfileDto.Put.Request request)
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
}

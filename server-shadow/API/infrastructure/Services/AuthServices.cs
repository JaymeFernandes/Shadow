using Microsoft.AspNetCore.Identity;
using Application.DTOs.Identity;
using Infrastructure.Contexts.Mongo;
using Domain.ObjectPool;
using Domain.Models.User;

namespace Infrastructure.Services;

public class AuthServices(UserManager<IdentityUser> userManager, MongoUserContext mongoUserContext, MongoPool mongoPool)
{
    private readonly UserManager<IdentityUser> _userManager =  userManager;
    private readonly MongoUserContext _mongoUserContext = mongoUserContext;
    private readonly MongoPool _mongoPool =  mongoPool;

    public async Task<RegisterDto.Response> CreateAsync(RegisterDto.Request register)
    {
        string? token = null;

        var user = new IdentityUser
        {
            Email = register.Email,
            UserName = register.Name,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, register.Password);

        if(result.Succeeded)
        {
            var mongoUser = new MongoUser
            {
                Code = user.Id,
                Display = register.Display,
                Name = register.Name,
                Lang = "en",
                LastLoginAt = DateTime.Now,
                isPrivate = false,
                CachedAt = DateTime.Now
            };

            _mongoPool.UsersQueue.Enqueue(mongoUser);
            
            token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        return new RegisterDto.Response
        {
            Succeeded = result.Succeeded,
            EmailCode = token ?? string.Empty,
            Errors = result.Errors.Select(x => x.Description).ToList()
        };
    }
}
using Microsoft.AspNetCore.Identity;
using Application.DTOs.Identity;
using Infrastructure.Contexts.Mongo;
using Domain.Models.User;
using Domain.Models.User.Shared;
using Infrastructure.Services.Save.Mongo.Users;

namespace Infrastructure.Services;

public class AuthServices(
    SignInManager<IdentityUser> signInManager, 
    UserManager<IdentityUser> userManager, 
    MongoUserContext mongoUserContext, 
    UserHandler userHandler)
{

    public async Task<RegisterResponse> CreateAsync(RegisterRequest register)
    {
        string? token = null;

        var user = new IdentityUser
        {
            Email = register.Email,
            UserName = register.Name,
            EmailConfirmed = false
        };

        var result = await userManager.CreateAsync(user, register.Password);

        if(result.Succeeded)
        {
            var mongoUser = new MongoUser
            {
                Code = user.Id,
                Display = register.Display,
                Name = register.Name,
                Lang = "en",
                LastLoginAt = DateTime.Now,
                IsPrivate = false,
                CachedAt = DateTime.Now
            };

            await userHandler.Add(mongoUser);
            
            token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        return new RegisterResponse
        {
            Succeeded = result.Succeeded,
            EmailCode = token ?? string.Empty,
            Errors = result.Errors.Select(x => x.Code).ToList()
        };
    }

    public async Task<bool> VerifyEmailsAsync(string email, string token)
    {
        var user = await userManager.FindByEmailAsync(email);

        if(user == null)
            return false;
        
        if (await userManager.IsEmailConfirmedAsync(user))
            return false;
        
        var result = await userManager.ConfirmEmailAsync(user, token);
        
        return result.Succeeded;
    }

    public async Task<(SignInResult, List<string>, IdentityUser?)> LoginAsync(string email, string password)
    {
        if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            return (SignInResult.Failed, new List<string>(), null);
        
        var user = await userManager.FindByEmailAsync(email);
        
        if (user == null)
            return (SignInResult.Failed, new(), null);
        
        if(!await signInManager.CanSignInAsync(user) || (userManager.SupportsUserLockout && await userManager.IsLockedOutAsync(user)))
            return (SignInResult.LockedOut, new(), null);
        
        var result = await signInManager.PasswordSignInAsync(user, password, false, true);

        if (result.Succeeded)
            await userManager.ResetAccessFailedCountAsync(user);
        
        var roles = await userManager.GetRolesAsync(user);
        
        return (result, roles.ToList(), user);
    }




    public async Task<bool> BanUserAsync(string actorId, string targetId, TimeSpan duration)
    {
        var (actor, actorRoles) = await GetUserByIdAsync(actorId);
        var (target, targetRoles) = await GetUserByIdAsync(targetId);

        if (actor is null || target is null)
            return false;

        if (!CanAffectUser(actorRoles, targetRoles))
            return false;

        await userManager.SetLockoutEndDateAsync(target, DateTimeOffset.UtcNow.Add(duration));
        
        return true;
    }

    public async Task<bool> UnbanUserAsync(string actorId, string targetId)
    {
        var (actor, actorRoles) = await GetUserByIdAsync(actorId);
        var (target, targetRoles) = await GetUserByIdAsync(targetId);

        if (actor is null || target is null)
            return false;

        if (!CanAffectUser(actorRoles, targetRoles))
            return false;
        
        await userManager.SetLockoutEndDateAsync(target, null);
        
        return true;
    }
    
    
    
    
    public async Task<(IdentityUser?, List<string>)> GetUserByIdAsync(string username)
    {
        var user = await userManager.FindByIdAsync(username);
        
        if (user == null)
            return (null, new List<string>());

        var roles = await userManager.GetRolesAsync(user);

        return (user, roles.ToList());
    }
    
    
    
    
    private static bool CanAffectUser(IList<string> actorRoles, IList<string> targetRoles)
    {
        var actorPower = actorRoles
            .Select(r => Enum.TryParse<Roles>(r, out var parsed) ? (int?)parsed : null)
            .Where(p => p.HasValue)
            .Min();

        var targetPower = targetRoles
            .Select(r => Enum.TryParse<Roles>(r, out var parsed) ? (int?)parsed : null)
            .Where(p => p.HasValue)
            .Min();

        if (actorPower is not null && targetPower is null)
            return actorPower <= (int)Roles.Moderator;

        if (actorPower is null || targetPower is null)
            return false;

        return actorPower < targetPower && actorPower <= (int) Roles.Moderator;
    }
    
    private static bool CanAffectRole(IList<string> actorRoles)
    {
        var actorPower = actorRoles
            .Select(r => Enum.TryParse<Roles>(r, out var parsed) ? (int?)parsed : null)
            .Where(p => p.HasValue)
            .Min();

        return actorPower == (int)Roles.Admin;
    }
       
}
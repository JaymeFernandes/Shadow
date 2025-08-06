using Api.Extensions;
using Application.DTOs;
using Application.DTOs.Admin.User;
using Domain.Models.User;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OpenIddict.Abstractions;

namespace Api.Controllers.Admin.User;


[ApiController]
[Tags("Admin")]
[Route("api/admin")]
[Authorize(Roles = "Admin,Moderator")]
public class AdminUserManagerController(UserManager<IdentityUser> userManager, AuthServices authServices, MongoUserContext context)
    : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> GetAllAsync([FromQuery] AllUserRequest request)
    {
        var filter = Builders<MongoUser>.Filter.Empty;
    
        if (!string.IsNullOrEmpty(request.Search))
        {
            var search = request.Search.ToLower();
            filter = Builders<MongoUser>.Filter.Where(f =>
                f.Name.ToLower().Contains(search) ||
                f.Display.ToLower().Contains(search) ||
                f.Code.ToLower().Contains(search)
            );
        }

        var users = await context.Users.Find(filter).ToListAsync();

        var paginatedUsers = users
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        var result = new List<AllUserResponse.User>();

        foreach (var x in paginatedUsers)
        {
            var dto = x.ToDtoAdmin();
            var user = await userManager.FindByIdAsync(x.Code);

            if (user is not null)
            {
                dto.Roles = (await userManager.GetRolesAsync(user)).ToList();
                dto.Email = user.Email;
                dto.IsEmailVerified = user.EmailConfirmed;
            }

            result.Add(dto);
        }

        return this.ApiResponseOk(data: result, meta: new
        {
            Page = request.Page,
            Size = request.PageSize,
            TotalSearchCount = users.Count
        });
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
    {
        var filter = Builders<MongoUser>.Filter.Eq("public_id", id);

        var userMongo = await context.Users.Find(filter).FirstOrDefaultAsync();

        var dto = userMongo.ToDtoAdmin();
        var user = await userManager.FindByIdAsync(dto.Id);

        if (user is not null)
        {
            dto.Roles = (await userManager.GetRolesAsync(user)).ToList();
            dto.Email = user.Email;
            dto.IsEmailVerified = user.EmailConfirmed;
        }
        
        return user is null
            ? NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Detail = "User not found"
            }) : this.ApiResponseOk(data: dto);
    }

    
    
    [HttpPost("user/ban")]
    public async Task<IActionResult> BanUserAsync([FromBody] BanRequest request)
    {
        var actor = HttpContext.User.FindFirst(OpenIddictConstants.Claims.Subject);
        
        if (actor is null)
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "You are not authenticated."
            });

        var result = await authServices.BanUserAsync(actor.Value, request.Id, request.Duration);
        
        return result ? this.ApiResponseOk<string>(title: "User banned successfully") : BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Ban Failed",
            Detail = "The user could not be banned. They may not exist or you may not have permission."
        });
    }
    
    [HttpPost("user/unban")]
    public async Task<IActionResult> UnbanUserAsync([FromBody] string id)
    {
        var actor = HttpContext.User.FindFirst(OpenIddictConstants.Claims.Subject);
        
        if (actor is null)
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "You are not authenticated."
            });

        var result = await authServices.UnbanUserAsync(actor.Value, id);
        
        return result ? this.ApiResponseOk<string>(title: "Reactive user successful") : BadRequest(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Ban Failed",
            Detail = "The user could not be unbanned. They may not exist or you may not have permission."
        });
    }

   
    
}
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Admin;

[ApiController]
[Tags("Admin")]
[Route("api/admin")]
[Authorize(Roles = "Admin,Moderator")]
public class AdminRoleManagerController(RoleManager<IdentityRole> roleManager) : ControllerBase
{
    [HttpGet("roles")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = roleManager.Roles.Select(x => new
        {
            Id = x.Id,
            Name = x.Name
        });

        return this.ApiResponseOk(data: roles, meta: new
        {
            Count = roles.Count(),
        });
    }
    
    
}
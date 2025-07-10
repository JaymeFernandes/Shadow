using Application.DTOs;
using Application.DTOs.Admin;
using Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Api.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[Tags("Admin")]
public class AdminUserManagerController : ControllerBase
{
    [HttpGet("users")]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] AllUserDto.Request request)
    {
        return Ok(new ApiResponse<string>(200));
    }

    [HttpGet("user")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById()
    {
        return Ok(new ApiResponse<string>(200));
    }
}
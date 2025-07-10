using Application.DTOs;
using Domain.Models.Post;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Api.Controller;

[ApiController]
public class IsRunningController : ControllerBase
{
    [HttpGet("")]
    public Task<IActionResult> HealthAsync()
        => Task.FromResult<IActionResult>(Ok(new ApiResponse<string>(StatusCodes.Status200OK, "Api is Running")));
    
    [HttpGet("ping")]
    public IActionResult Ping() => Ok("pong");

    [HttpGet("secure")]
    [Authorize]
    public IActionResult Secure() => Ok("you are authenticated");
}
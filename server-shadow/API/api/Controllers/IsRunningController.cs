using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class IsRunningController : ControllerBase
{
    [HttpGet("")]
    public Task<IActionResult> HealthAsync()
        => Task.FromResult<IActionResult>(this.ApiResponseOk<string>(title: "Api is Running"));
}
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class IsRunningController : ControllerBase
{
    [HttpGet("/")]
    public Task<IActionResult> HealthAsync()
        => Task.FromResult(this.ApiResponseOk<string>(title: "Api is Running", meta: new { isRunning = true, date = DateTime.UtcNow }));
}
using Application.DTOs;
using Application.DTOs.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Auth;

[Route("api/auth")]
[Tags("Authentication")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthServices _authServices;

    public AuthController(AuthServices authServices)
        => _authServices = authServices;

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto.Request register)
    {
        var result = await _authServices.CreateAsync(register);

        if (result.Succeeded)
            return Ok(
                new ApiResponse<RegisterDto.Response>(200, "Register new account", result)
                {
                    Meta = new
                    {
                        IsActive = false
                    }
                });

        var problemDetails = new ProblemDetails()
        {
            Title = "Account Creation Failed",
            Status = StatusCodes.Status400BadRequest,
            Detail = result.Errors?.FirstOrDefault() ?? string.Empty
        };

        return BadRequest(problemDetails);
    }
}
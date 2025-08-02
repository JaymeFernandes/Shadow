#nullable enable
using System.Security.Claims;
using Application.DTOs;
using Application.DTOs.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Api.Controllers.Auth;

[Route("connect")]
[Tags("Authentication")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly AuthServices _authServices;


    public AuthController(IOpenIddictScopeManager scopeManager, AuthServices authServices)
    {
        _scopeManager = scopeManager;
        _authServices = authServices;
    }


    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest register)
    {
        var result = await _authServices.CreateAsync(register);

        if (result.Succeeded)
            return this.ApiResponseOk(title: "Register new account", data: result, meta: new { IsActive = false });
        
        var problemDetails = new ProblemDetails()
        {
            Title = "Account Creation Failed",
            Status = StatusCodes.Status400BadRequest,
            Detail = result.Errors?.FirstOrDefault() ?? string.Empty
        };

        return BadRequest(problemDetails);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyEmailAsync([FromBody] VerifyEmailRequest request)
        => await _authServices.VerifyEmailsAsync(request.Email, request.Token) ? 
            this.ApiResponseOk<string>(title: "you have successful verified account") : 
            BadRequest(new ProblemDetails
            {
                Title = "Verification Failed",
                Status = StatusCodes.Status400BadRequest
            });

    [HttpPost("token")]
    public async Task<IActionResult> TokenAsync()
    {
        var openidConnectRequest = HttpContext.GetOpenIddictServerRequest()
            ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

        var identity = new ClaimsIdentity(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

        if (openidConnectRequest.IsPasswordGrantType())
        {
            var (result, roles, user) = 
                await _authServices.LoginAsync(openidConnectRequest.Username!, openidConnectRequest.Password!);

            if (result.IsLockedOut)
                return Unauthorized(new ProblemDetails
                {
                    Status = StatusCodes.Status423Locked,
                    Title = "Account is locked",
                    Detail = "Your account has been locked out"
                });

            if (!result.Succeeded)
                return Unauthorized(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "Username or password is incorrect"
                });

            identity.SetScopes(openidConnectRequest.GetScopes());

            identity = await GetIdentityAsync(user.Id, identity);
            
            if (identity == null)
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "The user profile no longer exists."
                });
            
            identity.SetDestinations(GetDestinations);
        }
        else if(openidConnectRequest.IsRefreshTokenGrantType())
        {
            var info = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            if (info.Principal == null)
                return Unauthorized(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = "The token is no longer valid"
                });
            
            var userId = info.Principal.GetClaim(OpenIddictConstants.Claims.Subject);
            
            identity.SetScopes(openidConnectRequest.GetScopes());
            identity = await GetIdentityAsync(userId, identity);

            if (identity == null)
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "The user profile no longer exists."
                });
            
            identity.SetDestinations(GetDestinations);
        }
        else
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Not Supported",
                Detail = "The specified grant type is not supported."
            });
        }

        return SignIn(new ClaimsPrincipal(identity), new AuthenticationProperties(),
            OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }
    
    private static IEnumerable<string> GetDestinations(Claim claim)
    {
        return claim.Type switch
        {
            OpenIddictConstants.Claims.Name or OpenIddictConstants.Claims.Subject => new[] { OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken },
            _ => new[] { OpenIddictConstants.Destinations.AccessToken }
        };
    }

    private async Task<ClaimsIdentity?> GetIdentityAsync(string userId, ClaimsIdentity identity)
    {
        identity.SetResources(await _scopeManager.ListResourcesAsync(identity.GetScopes()).ToListAsync());
        
        var (user, roles) = await _authServices.GetUserByIdAsync(userId);
        
        if(user == null)
            return null;
        
        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, user.Id));
        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Email, user.Email!));
        identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, user.UserName!));
            
        foreach (var role in roles)
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Role, role));

        return identity;
    }

}
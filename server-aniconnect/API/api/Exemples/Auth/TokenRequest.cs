using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Exemples.Auth;

public class TokenRequest
{
    [Required]
    [FromForm(Name = "grant_type")]
    public string Grant_Type { get; set; } = "password";

    [Required]
    [FromForm(Name = "username")]
    public string Username { get; set; }

    [Required]
    [FromForm(Name = "password")]
    public string Password { get; set; }
    
    [FromForm(Name = "client_id")]
    public string Client_Id { get; set; }

    [FromForm(Name = "scope")]
    public string? Scope { get; set; }
}

public class TokenRequestExample : IExamplesProvider<TokenRequest>
{
    public TokenRequest GetExamples()
    {
        return new TokenRequest
        {
            Grant_Type = "password",
            Username = "user@example.com",
            Password = "123456",
            Client_Id = "api-shadow",
            Scope = "api-shadow offline_access"
        };
    }
}
namespace Application.DTOs.Identity;

public class VerifyEmailRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}
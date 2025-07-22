namespace Application.DTOs.Identity;

public class VerifyEmailDto
{
    public class Request
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
namespace Application.DTOs.Profile;

public class AvatarDto
{
    public class Response
    {
        public string AvatarUrl { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; }
    }
}
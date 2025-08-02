namespace Application.DTOs.Profile;

public class GetProfileResponse
{
    public string? Display { get; set; }
    public string? Name { get; set; }
    public string? Lang { get; set; }
    public DateTime LastLoginAt { get; set; }
    public string? AvatarUrl { get; set; }
    public bool IsPrivate { get; set; }
}

public class PutProfileRequest
{
    public string? Display { get; set; }
            
    public string? Lang { get; set; }
            
    public bool IsPrivate { get; set; }
}
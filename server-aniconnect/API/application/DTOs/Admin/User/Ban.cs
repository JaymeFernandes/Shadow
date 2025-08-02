namespace Application.DTOs.Admin.User;

public class BanRequest
{
    public string Id { get; set; }
        
    public TimeSpan Duration { get; set; }
}
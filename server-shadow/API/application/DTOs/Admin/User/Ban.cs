namespace Application.DTOs.Admin.User;

public class Ban
{
    public class Request
    {
        public string Id { get; set; }
        
        public TimeSpan Duration { get; set; }
    }
}
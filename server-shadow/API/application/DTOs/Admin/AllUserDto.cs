using System.Text.Json.Serialization;

namespace Application.DTOs.Admin;

public class AllUserDto
{
    public class Request
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Search { get; set; } = string.Empty;
    }

    public class Response
    {
        public ICollection<User> Users { get; set; } = new List<User>();
        
        public class User
        {
            public Guid Id { get; set; }
        
            public string? AvatarUrl { get; set; }
        
            public string Display { get; set; } = string.Empty;
        
            public string Name { get; set; } = string.Empty;
        
            public string Lang { get; set; } = string.Empty;

            public bool IsPrivate { get; set; } = false;
        
            public string? Email { get; set; }
        
            public string? Phone { get; set; }
        
            public DateTime Created { get; set; }
            public DateTime LastLoginAt { get; set; }
        
            public bool EmailConfirmed { get; set; }
        
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public ICollection<string>? Roles { get; set; }
        
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public ICollection<Favorite>? Favorites { get; set; }
        
            public class Favorite
            {
                public int Id { get; set; }
            }
        }
    }
    
    public class Meta
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}
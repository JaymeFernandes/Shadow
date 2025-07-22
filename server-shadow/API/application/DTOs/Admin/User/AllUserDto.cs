using System.Text.Json.Serialization;

namespace Application.DTOs.Admin.User;

public class AllUserDto
{
    public class Request
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }

    public class Response
    {
        public ICollection<User> Users { get; set; } = new List<User>();
        
        public class User
        {
            public string? Id { get; set; }
            public string? AvatarUrl { get; set; }
            public string Display { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Lang { get; set; } = string.Empty;
            
            public bool IsPrivate { get; set; } = false;
            
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? Email { get; set; }
            public bool IsEmailVerified { get; set; } = false;
            
            public DateTime Created { get; set; }
            public DateTime LastLogin { get; set; }
            
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
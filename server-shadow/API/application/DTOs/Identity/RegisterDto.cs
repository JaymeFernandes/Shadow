using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.DTOs.Identity;

public class RegisterDto
{
    public class Request
    {
        [Required, MaxLength(100)]
        public string Display { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress, Required, MaxLength(120)]
        public string Email { get; set; }
        
        
        [Required, MaxLength(50), MinLength(8)]
        public string Password { get; set; }
    }

    public class Response
    {
        // [JsonIgnore]
        public string EmailCode {  get; set; } = string.Empty;

        public bool Succeeded { get; set; }

        public List<string>? Errors { get; set; }
    }
}
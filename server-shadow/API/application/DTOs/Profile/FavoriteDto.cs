using System.ComponentModel.DataAnnotations;
using Domain.Models.User;
using Domain.Models.User.Shared;

namespace Application.DTOs.Profile;

public class FavoriteDto
{
    public class Get
    {
        public class Request
        {
            public string? Query { get; set; }
            
            [Required]
            public Favorite? Category { get; set; }
        }
    }
}
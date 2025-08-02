using System.ComponentModel.DataAnnotations;
using Domain.Models.User;
using Domain.Models.User.Shared;

namespace Application.DTOs.Profile;

public class GetFavoriteRequest
{
    public string? Query { get; set; }
            
    [Required]
    public Favorite? Category { get; set; }
}
using Domain.Models.Content.Enum;

namespace Domain.Models.Content.Metadata.Shared;

public class CharacterEfBase
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? OriginalName { get; set; }
    public int HeightCm { get; set; }
    public ZodiacSign ZodiacSign{ get; set; } 
}
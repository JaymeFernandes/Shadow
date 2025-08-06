namespace Domain.Models.Content.Metadata.Shared;

public class ImageEfBase
{
    public int Id { get; set; }
    
    public string? Link { get; set; }
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
}
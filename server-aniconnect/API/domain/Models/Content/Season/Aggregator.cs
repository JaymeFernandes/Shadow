namespace Domain.Models.Content.Season;

public class Aggregator
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }

    public string? Avatar { get; set; }
    
    public ICollection<ExternalLink>? ExternalLinks { get; set; }
}
namespace Domain.Models.Content.Season;

public class Episode
{
    public Guid Id { get; set; }
    public string? Name { get; set; }  
  
    public double Sequence { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    #region # Relations

    public Guid SeasonId { get; set; }
    public Guid ContentId { get; set; }
    
    
    
    public Season? Season { get; set; }
    public Content? Content { get; set; }
    
    public ICollection<ExternalLink>? ExternalLinks { get; set; }

    #endregion
}
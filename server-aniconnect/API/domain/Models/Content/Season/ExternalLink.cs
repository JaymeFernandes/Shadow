namespace Domain.Models.Content.Season;

public class ExternalLink
{
    public int Id { get; set; }

    #region # Relations

    public Guid EpisodeId { get; set; }
    public Guid AggregatorId { get; set; }
    public Guid ContentId { get; set; }
    
    public virtual Episode Episode { get; set; }
    public virtual Aggregator Aggregator { get; set; }
    public virtual Content Content { get; set; }

    #endregion
}
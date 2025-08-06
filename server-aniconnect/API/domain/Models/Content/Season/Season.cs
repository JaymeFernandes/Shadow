namespace Domain.Models.Content.Season;

public class Season
{
    public Guid Id { get; set; }
    public int SeasonNumber { get; set; }

    #region # Relations

    public Guid ContentId { get; set; }
    
    
    public virtual Content? Content { get; set; }
    public virtual ICollection<Episode>? Episodes { get; set; }

    #endregion
}
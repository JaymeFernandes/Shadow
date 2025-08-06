using Domain.Models.Content.Enum;

namespace Domain.Models.Content.Metadata;

public class Media
{
    public int Id { get; set; }

    public MediaType Type { get; set; }
    
    public string Url { get; set; } = string.Empty;

    public string Platform { get; set; } = string.Empty; 

    public string Thumbnail { get; set; } = string.Empty; 

    public string Title { get; set; } = string.Empty;

    #region # Relations

    public Guid ContentId { get; set; }
    public virtual Content? Content { get; set; }

    #endregion
}
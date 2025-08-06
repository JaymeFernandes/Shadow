using Domain.Models.Content.Metadata.Shared;

namespace Domain.Models.Content.Metadata;

public class Character : CharacterEfBase
{
    #region # Relations

    public Guid ContentId { get; set; }
    
    public virtual Content? Content { get; set; }
    public virtual ICollection<Actor>? Actors { get; set; }

    #endregion
}
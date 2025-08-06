using Domain.Models.Content.Metadata.Shared;

namespace Domain.Models.Content.Metadata;

public class Actor : CharacterEfBase
{
    #region # Relations

    public virtual ICollection<Character>? Characters { get; set; }

    #endregion
}
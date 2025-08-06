namespace Domain.Models.Content.Common.RelationObject;

public class CategoryByContent
{

    #region # Relation

    public int CategoryId { get; set; }
    public Guid ContentId { get; set; }

    #endregion
}
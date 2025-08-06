namespace Domain.Models.Content.Common.RelationObject;

public class TagByContent
{
    #region # Relation

    public int TagId { get; set; }
    public Guid ContentId { get; set; }

    #endregion
}
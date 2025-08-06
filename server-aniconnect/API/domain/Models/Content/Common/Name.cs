namespace Domain.Models.Content.Common;

public class Name
{
    public int Id { get; set; }
    
    public string? Value { get; set; }
    
    public string? Lang { get; set; }

    #region Relations

    public Guid ContentId { get; set; }

    #endregion
}
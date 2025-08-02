namespace Domain.Models.Works;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public virtual Work Work { get; set; }

    public int workId { get; set; }
}
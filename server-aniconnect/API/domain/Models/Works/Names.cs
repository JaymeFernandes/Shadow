namespace Domain.Models.Works;

public class Names
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Lang { get; set; } = string.Empty;

    public int WorkId { get; set; }
    public virtual Work? Work { get; set; }
}
namespace Domain.Models.Works;

public class Chapter
{
    public int Id { get; set; }
    public string Code { get; set; }
        
    public string Name { get; set; }

    public double Sequence { get; set; }

    public DateTime CreatedAt { get; set; }

    public int WorkId { get; set; }
}
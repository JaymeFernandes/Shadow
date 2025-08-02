namespace Domain.Models.Works;

public class Work
{
    public int Id { get; set; }                   
    public Guid Code { get; set; }    
    public Status Status { get; set; }

        
    public virtual ICollection<Names>? Names { get; set; }
    public string Description { get; set; } = string.Empty;

    public virtual ICollection<Author> Authors { get; set; }

    public string Cover { get; set; } = string.Empty;       
    public string Background { get; set; } = string.Empty;  

    public int UsersAvaliable { get; set; }                 
    public long TotalScore { get; set; }

    public double AverageScore => UsersAvaliable == 0 ? 0 : (double)TotalScore / UsersAvaliable;


    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }

    public DateTime? EndDate { get; set; }


    public virtual ICollection<Chapter>? Chapters { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
    public virtual ICollection<Tag>? Tags { get; set; }
}
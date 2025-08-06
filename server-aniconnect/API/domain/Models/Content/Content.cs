using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Content.Common;
using Domain.Models.Content.Enum;
using Domain.Models.Content.Metadata;

namespace Domain.Models.Content;

public class Content
{
    public Guid Id { get; set; }  
  
    public Status Status { get; set; }  
    public ContentType Type { get; set; }
	  
    public string Description { get; set; } = string.Empty;  
    
    public int UsersAvaliable { get; set; }                 
    public long TotalScore { get; set; }  
	 
    [NotMapped]
    public double AverageScore => UsersAvaliable == 0 ? 0 : (double)TotalScore / UsersAvaliable;  
	  
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;  
    public DateTime? UpdatedDate { get; set; }  
	  
    public DateTime? EndDate { get; set; }



    #region # Relations

    public virtual ICollection<Season.Season>? Seasons { get; set; }
    public virtual ICollection<Name>? Names { get; set; }
    public virtual ICollection<Category>? Categories { get; set; }
    public virtual ICollection<Tag>? Tags { get; set; }
    public virtual ICollection<Media>? Medias { get; set; }
    public virtual ICollection<Character>? Characters { get; set; }

    #endregion
    
    
}
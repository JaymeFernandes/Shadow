using Domain.Models.Works;

namespace Application.DTOs.Admin.Work;

public class RequestNewWork
{
    public Status Status { get; set; }
    
    public ICollection<RequestNemesNewWork> Names { get; set; }
    
    public string? Description { get; set; }
    
    public ICollection<string> Authors { get; set; }
    
    public ICollection<int>? Categories { get; set; }
    public ICollection<int>? Tags { get; set; }
}

public class RequestNemesNewWork
{
    public string Name { get; set; }
    public string Lang { get; set; }
}
namespace Infrastructure.Interfaces.Content;

public interface IContentRepository
{
    public Task<Domain.Models.Content.Content?> GetContentAsync(string id);
    
    public Task WriteContentAsync(Domain.Models.Content.Content content);
    public Task<bool> DeleteContentAsync(string id);
}
using Domain.Models.Works;

namespace Infrastructure.Interfaces.Work;

public interface IWorkRepository
{
    // Work
    Task<Domain.Models.Works.Work?> GetWorkByIdAsync(int id);
    Task PostWorkAsync(Domain.Models.Works.Work work);
    Task<bool> DeleteWorkByIdAsync(int id);
    
    
    // Chapter
    Task<Chapter?> GetChapterByIdAsync(int id);
    Task PostChapterAsync(Chapter chapter);
    Task<bool> DeleteChapterAsync(int id);
    
    // Author
    Task PostAuthorAsync(Author author);
    Task<bool> DeleteAuthorAsync(int authorId);
    
    // Name
    Task PostNameAsync(Names name);
    Task<bool> DeleteNameAsync(int id);
    
    // Catalog
    Task<List<Category>> GetAllCategoriesAsync();
    Task AddCategoryInWorkAsync(int work, int category);
    
    Task<List<Tag>> GetAllTagsAsync();
    Task AddTagInWorkAsync(int work, int tag);
}
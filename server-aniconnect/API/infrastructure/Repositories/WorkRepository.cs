using Domain.Models.Works;
using Infrastructure.Contexts.Works;
using Infrastructure.Interfaces.Work;
using Infrastructure.Services.Save.EfCore.Work;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly WorkAppContext _context;
    
    private readonly WorkHandler _work;
    private readonly ChaptersHandler _chapters;
    private readonly AuthorsHandler _authors;
    private readonly NamesHandler _names;
    private readonly WorkCategoryHandler _workCategory;
    private readonly WorkTagHandler _workTag;

    public WorkRepository(
        WorkAppContext context, 
        WorkHandler work,
        ChaptersHandler chapters, 
        AuthorsHandler authors, 
        NamesHandler names, 
        WorkCategoryHandler workCategory, 
        WorkTagHandler workTag)
    {
        _context = context;
        _work = work;
        _chapters = chapters;
        _authors = authors;
        _names = names;
        _workCategory = workCategory;
        _workTag = workTag;
    }


    #region # Work
    
    public async Task<Work?> GetWorkByIdAsync(int id)
        => await _context.Works.AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Names)
            .Include(x => x.Authors)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task PostWorkAsync(Work work)
        => await _work.Add(work);

    public async Task<bool> DeleteWorkByIdAsync(int id)
    {
        var entity = new Work {  Id = id };

        _context.Attach(entity);
        _context.Works.Remove(entity);
        
        return await _context.SaveChangesAsync() > 0;
    }
    
    #endregion

    #region # Chapter

    public async Task<Chapter?> GetChapterByIdAsync(int id)
        => await _context.Chapters.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    
    
    public async Task PostChapterAsync(Chapter chapter) 
        => await _chapters.Add(chapter);
    
    
    public async Task<bool> DeleteChapterAsync(int id)
    {
        var entity = new Chapter {  Id = id };
        _context.Attach(entity);
        _context.Chapters.Remove(entity);
        
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion
    
    # region # Author

    public async Task PostAuthorAsync(Author author)
        => await _authors.Add(author);

    public async Task<bool> DeleteAuthorAsync(int authorId)
    {
        var entity = new Author {  Id = authorId };
        _context.Attach(entity);
        _context.Authors.Remove(entity);
        
        return await _context.SaveChangesAsync() > 0;
    }
    
    # endregion
    
    #region # Name

    public async Task PostNameAsync(Names name)
        => await _names.Add(name);

    public async Task<bool> DeleteNameAsync(int id)
    {
        var entity = new Names { Id = id };

        _context.Attach(entity);
        _context.Names.Remove(entity);
        
        return await _context.SaveChangesAsync() > 0;
    }

    #endregion
    
    #region # Catalog

    public async Task<List<Category>> GetAllCategoriesAsync()
        => await _context.Categories.AsNoTracking().ToListAsync();

    public async Task AddCategoryInWorkAsync(int work, int category)
    {
        var entity = new Category()
        {
            Code = category,
            WorkId = work
        };

        await _workCategory.Add(entity);
    }

    public async Task<List<Tag>> GetAllTagsAsync()
        => await _context.Tags.AsNoTracking().ToListAsync();

    public async Task AddTagInWorkAsync(int work, int tag)
    {
        var entity = new Tag
        {
            WorkId = work,
            Code = tag
        };
        
        await _workTag.Add(entity);
    }

    #endregion
}
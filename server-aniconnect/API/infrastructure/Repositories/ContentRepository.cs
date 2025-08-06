using Domain.Models.Content;
using EFCore.BulkExtensions;
using Infrastructure.Contexts.Content;
using Infrastructure.Interfaces.Content;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly ContentAppContext _context;

    public ContentRepository(ContentAppContext context)
    {
        _context = context;
    }


    public async Task<Content?> GetContentAsync(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return null;
        
        return await _context.Contents
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Include(x => x.Names)
            .FirstOrDefaultAsync(x => x.Id == guid);
    }

    public async Task WriteContentAsync(Content content)
    {
        await _context.BulkInsertOrUpdateAsync(new List<Content>() { content });
    }

    public async Task<bool> DeleteContentAsync(string id)
    {
        if (!Guid.TryParse(id, out var guid))
            return false;
        
        var entity = new Content {  Id = guid };

        _context.Attach(entity);
        _context.Contents.Remove(entity);
         
         return await _context.SaveChangesAsync() > 0;
    }
}
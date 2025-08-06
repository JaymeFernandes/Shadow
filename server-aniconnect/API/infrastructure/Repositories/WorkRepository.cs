namespace Infrastructure.Repositories;

// public class WorkRepository //: IWorkRepository
// {
//     private readonly ContentAppContext _context;
//     
//     private readonly WorkHandler _work;
//     private readonly ChaptersHandler _chapters;
//     private readonly AuthorsHandler _authors;
//     private readonly NamesHandler _names;
//     private readonly WorkCategoryHandler _workCategory;
//     private readonly WorkTagHandler _workTag;
//
//     public WorkRepository(
//         ContentAppContext context, 
//         WorkHandler work,
//         ChaptersHandler chapters, 
//         AuthorsHandler authors, 
//         NamesHandler names, 
//         WorkCategoryHandler workCategory, 
//         WorkTagHandler workTag)
//     {
//         _context = context;
//         _work = work;
//         _chapters = chapters;
//         _authors = authors;
//         _names = names;
//         _workCategory = workCategory;
//         _workTag = workTag;
//     }
//
//
//     #region # Content
//     
//     public async Task<Content?> GetWorkByIdAsync(int id)
//         => await _context.Content.AsNoTracking()
//             .AsSplitQuery()
//             .FirstOrDefaultAsync();
//     
//     public async Task<Content?> GetWorkByCodeAsync(string code)
//         => await _context.Content.AsNoTracking()
//             .AsSplitQuery()
//             .FirstOrDefaultAsync(x => x.Id == Guid.Parse(code));
//     
//
//     public async Task PostWorkAsync(Content content)
//         => await _work.Add(content);
//
//     public async Task<bool> DeleteWorkByIdAsync(int id)
//     {
//         var entity = new Content {  Id = id };
//
//         _context.Attach(entity);
//         _context.Content.Remove(entity);
//         
//         return await _context.SaveChangesAsync() > 0;
//     }
//     
//     #endregion
//
//     #region # Episode
//
//     public async Task<Episode?> GetChapterByIdAsync(int id)
//         => await _context.Chapters.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
//     
//     
//     public async Task PostChapterAsync(Episode episode) 
//         => await _chapters.Add(episode);
//     
//     
//     public async Task<bool> DeleteChapterAsync(int id)
//     {
//         var entity = new Episode {  Id = id };
//         _context.Attach(entity);
//         _context.Chapters.Remove(entity);
//         
//         return await _context.SaveChangesAsync() > 0;
//     }
//
//     #endregion
//     
//     # region # Author
//
//     public async Task PostAuthorAsync(Author author)
//         => await _authors.Add(author);
//
//     public async Task<bool> DeleteAuthorAsync(int authorId)
//     {
//         var entity = new Author {  Id = authorId };
//         _context.Attach(entity);
//         _context.Authors.Remove(entity);
//         
//         return await _context.SaveChangesAsync() > 0;
//     }
//     
//     # endregion
//     
//     #region # Name
//
//     public async Task PostNameAsync(Names name)
//         => await _names.Add(name);
//
//     public async Task<bool> DeleteNameAsync(int id)
//     {
//         var entity = new Names { Id = id };
//
//         _context.Attach(entity);
//         _context.Names.Remove(entity);
//         
//         return await _context.SaveChangesAsync() > 0;
//     }
//
//     #endregion
//     
//     #region # Catalog
//
//     public async Task<List<Category>> GetAllCategoriesAsync()
//         => await _context.Categories.AsNoTracking().ToListAsync();
//
//     public async Task AddCategoryInWorkAsync(int work, int category)
//     {
//         var entity = new Category()
//         {
//             Code = category,
//             WorkId = work
//         };
//
//         await _workCategory.Add(entity);
//     }
//
//     public async Task<List<Tag>> GetAllTagsAsync()
//         => await _context.Tags.AsNoTracking().ToListAsync();
//
//     public async Task AddTagInWorkAsync(int work, int tag)
//     {
//         var entity = new Tag
//         {
//             WorkId = work,
//             Code = tag
//         };
//         
//         await _workTag.Add(entity);
//     }
//
//     #endregion
// }
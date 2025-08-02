using Microsoft.EntityFrameworkCore;
using Infrastructure.Mappings.Works;
using Domain.Models.Works;


namespace Infrastructure.Contexts.Works;

public class WorkAppContext : DbContext
{
    public WorkAppContext(DbContextOptions<WorkAppContext> options) : base(options) { }

    public virtual DbSet<Names> Names { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Chapter> Chapters { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Work> Works { get; set; }
    
    public virtual DbSet<Author> Authors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new NameMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new ChapterMap());
        modelBuilder.ApplyConfiguration(new TagMap());
        modelBuilder.ApplyConfiguration(new WorkMap());
        modelBuilder.ApplyConfiguration(new AuthorMap());


        base.OnModelCreating(modelBuilder);
    }
}
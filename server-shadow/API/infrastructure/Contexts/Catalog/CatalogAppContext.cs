using Microsoft.EntityFrameworkCore;
using Domain.Models.Catalog;
using Infrastructure.Mappings.Catalog;

namespace Infrastructure.Contexts.Catalog;

public class CatalogAppContext : DbContext
{
    public CatalogAppContext(DbContextOptions<CatalogAppContext> options) : base(options) { }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }

        
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new TagMap());

        base.OnModelCreating(modelBuilder);
    }
}
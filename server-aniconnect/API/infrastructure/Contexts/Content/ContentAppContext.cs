using Domain.Models.Content.Common;
using Domain.Models.Content.Metadata;
using Domain.Models.Content.Season;
using Infrastructure.Mappings.Content;
using Infrastructure.Mappings.Content.Common;
using Infrastructure.Mappings.Content.Metadata;
using Infrastructure.Mappings.Content.Season;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts.Content;

public class ContentAppContext : DbContext
{
    public virtual DbSet<Domain.Models.Content.Content> Contents { get; set; }
    public virtual DbSet<Name> Names { get; set; }
    public virtual DbSet<Cover> Covers { get; set; }
    public virtual DbSet<BackGround> BackGrounds { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Tag> Tags { get; set; }
    public virtual DbSet<Season> Seasons { get; set; }
    public virtual DbSet<Episode> Episodes { get; set; }
    public virtual DbSet<ExternalLink> ExternalLinks { get; set; }
    public virtual DbSet<Aggregator> Aggregators { get; set; }
    public virtual DbSet<Media> Medias { get; set; }
    public virtual DbSet<Actor> Actors { get; set; }
    public virtual DbSet<Character> Characters { get; set; }
    
    
    public ContentAppContext(DbContextOptions<ContentAppContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ContentMap());
        modelBuilder.ApplyConfiguration(new NameMap());
        modelBuilder.ApplyConfiguration(new ImageEfBaseMap<Cover>());
        modelBuilder.ApplyConfiguration(new ImageEfBaseMap<BackGround>());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new TagMap());
        modelBuilder.ApplyConfiguration(new SeasonMap());
        modelBuilder.ApplyConfiguration(new EpisodeMap());
        modelBuilder.ApplyConfiguration(new ExternalLinkMap());
        modelBuilder.ApplyConfiguration(new AggregatorMap());
        modelBuilder.ApplyConfiguration(new MediaMap());
        modelBuilder.ApplyConfiguration(new CharacterEfBaseMap<Actor>());
        modelBuilder.ApplyConfiguration(new CharacterEfBaseMap<Character>());
        modelBuilder.ApplyConfiguration(new CharacterMap());
        
        
        base.OnModelCreating(modelBuilder);
    }
}
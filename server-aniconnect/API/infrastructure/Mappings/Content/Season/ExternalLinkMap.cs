using Domain.Models.Content.Season;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Season;

public class ExternalLinkMap : IEntityTypeConfiguration<ExternalLink>
{
    public void Configure(EntityTypeBuilder<ExternalLink> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Aggregator)
            .WithMany(x => x.ExternalLinks)
            .HasForeignKey(x => x.AggregatorId);
        
        builder.HasOne(x => x.Episode)
            .WithMany(x => x.ExternalLinks)
            .HasForeignKey(x => x.EpisodeId);
        
        builder.HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId);
    }
}
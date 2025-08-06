using Domain.Models.Content.Season;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Season;

public class EpisodeMap : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Sequence)
            .IsRequired();

        builder.HasIndex(e => new { e.SeasonId, e.Sequence })
            .IsUnique();
        
        builder.HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId);
    }
}
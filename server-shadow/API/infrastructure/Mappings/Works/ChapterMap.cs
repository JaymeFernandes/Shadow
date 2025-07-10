using Domain.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Works;

public class ChapterMap : IEntityTypeConfiguration<Chapter>
{
    public void Configure(EntityTypeBuilder<Chapter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => x.Code);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Sequence)
            .IsRequired();

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.WorkId).IsRequired();

        builder.ToTable("Chapters");
    }
}
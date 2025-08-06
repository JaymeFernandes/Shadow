using Domain.Models.Content.Enum;
using Domain.Models.Content.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Metadata;

public class MediaMap : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Type)
            .HasDefaultValue(MediaType.Etc);

        builder.Property(x => x.Url)
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(x => x.Platform)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Thumbnail)
            .HasMaxLength(2048)
            .IsRequired();
        
        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
    }
}
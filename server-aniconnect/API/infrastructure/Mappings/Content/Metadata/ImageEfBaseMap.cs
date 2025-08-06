using Domain.Models.Content.Metadata.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Metadata;

public class ImageEfBaseMap<T> : IEntityTypeConfiguration<T> where T : ImageEfBase
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Link)
            .HasMaxLength(2048)
            .IsRequired();
    }
}
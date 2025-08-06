using Domain.Models.Content.Enum;
using Domain.Models.Content.Metadata.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Metadata;

public class CharacterEfBaseMap<T> : IEntityTypeConfiguration<T> where T : CharacterEfBase
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.OriginalName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.HeightCm)
            .HasDefaultValue(0);

        builder.Property(x => x.ZodiacSign)
            .HasDefaultValue(ZodiacSign.Undefined);
    }
}
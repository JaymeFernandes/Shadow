using Domain.Models.Content.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Common;

public class NameMap : IEntityTypeConfiguration<Name>
{
    public void Configure(EntityTypeBuilder<Name> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Value)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Lang)
            .HasMaxLength(7)
            .IsRequired();
    }
}
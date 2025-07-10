using Domain.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Works;

public class NameMap : IEntityTypeConfiguration<Names>
{
    public void Configure(EntityTypeBuilder<Names> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();


        builder.Property(x => x.WorkId)
            .IsRequired();

        builder.ToTable("Names");
    }
}
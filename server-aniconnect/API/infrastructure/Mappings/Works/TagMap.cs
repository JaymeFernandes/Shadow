using Domain.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Works;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Code);

        builder.Property(x => x.Code)
            .IsRequired();

        builder.ToTable("Tags");
    }
}
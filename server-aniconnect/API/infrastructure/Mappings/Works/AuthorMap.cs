using Domain.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Works;

public class AuthorMap : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(x => x.Work)
            .WithMany(x => x.Authors)
            .HasForeignKey(x => x.workId);
    }
}
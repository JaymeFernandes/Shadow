using Domain.Models.Works;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Works;

public class WorkMap : IEntityTypeConfiguration<Work>
{
    public void Configure(EntityTypeBuilder<Work> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.HasAlternateKey(x => x.Code);

        builder.Property(x => x.Status)
            .HasDefaultValue(Status.Ongoing)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Cover)
            .HasMaxLength(500);

        builder.Property(x => x.Background)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate)
            .IsRequired(false);

        builder.HasMany(x => x.Chapters)
            .WithOne()
            .HasForeignKey(x => x.WorkId);

        builder.HasMany(x => x.Names)
            .WithOne(x => x.Work)
            .HasForeignKey(x => x.WorkId);

        builder.HasMany(x => x.Categories)
            .WithOne()
            .HasForeignKey(x => x.WorkId);

        builder.HasMany(x => x.Tags)
            .WithOne()
            .HasForeignKey(x => x.WorkId);

        builder.ToTable("Works");
    }
}
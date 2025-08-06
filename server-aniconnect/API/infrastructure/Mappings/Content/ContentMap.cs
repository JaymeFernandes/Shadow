using Domain.Models.Content.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content;

public class ContentMap : IEntityTypeConfiguration<Domain.Models.Content.Content>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Content.Content> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasDefaultValue(Status.Ongoing);

        builder.Property(x => x.Type)
            .HasDefaultValue(ContentType.Manga);

        builder.Property(x => x.Description)
            .HasMaxLength(2000)
            .HasDefaultValue(string.Empty);
        
        builder.Property(x => x.UsersAvaliable)
            .HasDefaultValue(0);
        
        builder.Property(x => x.TotalScore)
            .HasDefaultValue(0);

        builder.Property(x => x.CreatedDate)
            .IsRequired();

        builder.Property(x => x.UpdatedDate)
            .HasDefaultValue(null);
        
        builder.Property(x => x.EndDate)
            .HasDefaultValue(null);
        
        builder.HasMany(x => x.Seasons)
            .WithOne(x => x.Content)
            .HasForeignKey(x => x.ContentId);
        
        builder.HasMany(x => x.Names)
            .WithOne()
            .HasForeignKey(x => x.ContentId);
        
        builder.HasMany(x => x.Medias)
            .WithOne(x => x.Content)
            .HasForeignKey(x => x.ContentId);
        
        builder.HasMany(x => x.Characters)
            .WithOne(x => x.Content)
            .HasForeignKey(x => x.ContentId);
    }
}
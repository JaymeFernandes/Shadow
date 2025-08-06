using Domain.Models.Content.Common;
using Domain.Models.Content.Common.RelationObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Common;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany<Domain.Models.Content.Content>()
            .WithMany()
            .UsingEntity<TagByContent>(
                x => x.HasOne<Domain.Models.Content.Content>()
                    .WithMany().HasForeignKey(t => t.ContentId),
                y => y.HasOne<Tag>()
                    .WithMany().HasForeignKey(t => t.TagId),
                z =>
                {
                    z.HasKey(x => new { x.ContentId, x.TagId });

                    z.ToTable("TagByContent");
                });

        builder.ToTable("Tags");
    }
}
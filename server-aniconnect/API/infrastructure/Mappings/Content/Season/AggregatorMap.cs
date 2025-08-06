using Domain.Models.Content.Season;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Season;

public class AggregatorMap : IEntityTypeConfiguration<Aggregator>
{
    public void Configure(EntityTypeBuilder<Aggregator> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200);

        builder.Property(x => x.Avatar)
            .HasMaxLength(2048);
    }
}
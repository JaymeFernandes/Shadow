using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Season;

public class SeasonMap : IEntityTypeConfiguration<Domain.Models.Content.Season.Season>
{
    public void Configure(EntityTypeBuilder<Domain.Models.Content.Season.Season> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.SeasonNumber)
            .HasDefaultValue(1);
        
        builder.HasMany(x => x.Episodes)
            .WithOne(x => x.Season)
            .HasForeignKey(x => x.SeasonId);
    }
}
using Domain.Models.Content.Metadata;
using Domain.Models.Content.Metadata.RelationObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings.Content.Metadata;

public class CharacterMap : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasMany(x => x.Actors)
            .WithMany(x => x.Characters)
            .UsingEntity<CharacterByActor>(
                x => x.HasOne<Actor>().WithMany().HasForeignKey(x => x.ActorId),
                y => y.HasOne<Character>().WithMany().HasForeignKey(x => x.CharacterId),
                z =>
                {
                    z.HasKey(x => new { x.CharacterId, x.ActorId });
                    z.ToTable("CharacterByActor");
                }
            );
    }
}
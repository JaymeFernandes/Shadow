namespace Domain.Models.Content.Metadata.RelationObject;

public class CharacterByActor
{
    public Guid ActorId { get; set; }
    public Guid CharacterId { get; set; }
}
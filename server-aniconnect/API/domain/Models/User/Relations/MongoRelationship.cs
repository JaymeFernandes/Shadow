using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User.Relations;

public class MongoRelationship
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonElement("user1")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string User1 { get; set; } = string.Empty;

    [BsonElement("user2")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string User2 { get; set; } = string.Empty;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("status")]
    public MongoStatus Status { get; set; }
}
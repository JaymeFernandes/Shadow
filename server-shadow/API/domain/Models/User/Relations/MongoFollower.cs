using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User.Relations;

public class MongoFollower
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    [BsonElement("user_id")]
    public string UserId { get; set; } = string.Empty; 

    [BsonElement("follower_id")]
    public string FollowerId { get; set; } = string.Empty;

    [BsonElement("followed_at")]
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
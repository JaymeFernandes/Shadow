using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User;

public class MongoFollower
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("user_id")]
    public string UserId { get; set; } = string.Empty; 

    [BsonElement("follower_id")]
    public string FollowerId { get; set; } = string.Empty;

    [BsonElement("followed_at")]
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}
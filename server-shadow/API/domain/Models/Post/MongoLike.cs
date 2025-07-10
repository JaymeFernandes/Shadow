using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Post;

public class MongoLike
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("post_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string PostId { get; set; } = string.Empty;

    [BsonElement("comment_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonIgnoreIfDefault]
    public string CommentId { get; set; } = string.Empty;

    [BsonElement("user_id")]
    public string UserId { get; set; } = string.Empty;

    [BsonElement("liked_at")]
    public DateTime LikedAt { get; set; } = DateTime.UtcNow;
}
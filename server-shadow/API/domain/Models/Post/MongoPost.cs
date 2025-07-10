using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Post;

public class MongoPost
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("user_id")]
    public string Code { get; set; } = string.Empty;

    [BsonElement("author")]
    public string Author { get; set; }

    [BsonElement("work_id")]
    [BsonIgnoreIfNull]
    public int Work { get; set; } 

    [BsonElement("created")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("lang")]
    public string Lang { get; set; }

    [BsonElement("content")]
    public string Content { get; set; }

    [BsonElement("categories")]
    public ICollection<MongoCategory> Categories { get; set; } = new HashSet<MongoCategory>();

    [BsonElement("tags")]
    public ICollection<MongoTag> Tags { get; set; } = new HashSet<MongoTag>();

    [BsonElement("comments")]
    public ICollection<MongoComment> Comments { get; set; } = new HashSet<MongoComment>();
}
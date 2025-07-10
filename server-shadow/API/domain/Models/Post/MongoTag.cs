using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Post;

public class MongoTag
{
    [BsonElement("code")]
    public int Code { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
}
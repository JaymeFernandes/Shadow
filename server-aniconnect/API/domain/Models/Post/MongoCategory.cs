using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.Post;

public class MongoCategory
{
    [BsonElement("code")]
    public int Code { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;
}
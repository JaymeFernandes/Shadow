using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User;

public class MongoFavorite
{
    [BsonElement("work")]
    public int Work { get; set; }

    [BsonElement("chapters")]
    [BsonIgnoreIfNullAttribute]
    public ICollection<MongoChapters>? Chapters { get; set; }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User;

public class MongoUser
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("public_id")]
    public string Code { get; set; } = string.Empty;

    [BsonElement("display")]
    public string Display { get; set; } = string.Empty; 

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty; 

    [BsonElement("lang")]
    public string Lang { get; set; } = string.Empty;

    [BsonElement("last_login")]
    public DateTime LastLoginAt { get; set; }

    [BsonElement("avatar")]
    [BsonIgnoreIfNull]
    public string? AvatarUrl { get; set; }

    [BsonElement("is_private")]
    public bool isPrivate { get; set; }

    [BsonElement("favorites")]
    [BsonIgnoreIfNull]
    public ICollection<MongoFavorite>? Favorites { get; set; }

    [BsonElement("cached_at")]
    public DateTime CachedAt { get; set; } = DateTime.UtcNow;
}
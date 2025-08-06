using Domain.Models.User.Shared;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User;

public class MongoFavorite
{
    [BsonElement("content")] 
    public string Work { get; set; } = string.Empty;

    [BsonElement("chapters")]
    public ICollection<string>? Chapters { get; set; }

    [BsonElement("category")] 
    public Favorite Category { get; set; } = Favorite.Favorite;
}
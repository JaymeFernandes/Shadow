using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Models.User;

public class MongoBlackList
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
    
    [BsonElement("user_id")]
    public string Code { get; set; } = String.Empty;
    
    [BsonElement("expire")]
    [BsonIgnoreIfNull]
    public DateTime? Expire { get; set; }
}
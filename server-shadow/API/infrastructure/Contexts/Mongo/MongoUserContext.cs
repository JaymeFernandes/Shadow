using Domain.Models.User;
using Domain.Models.User.Relations;
using Domain.Options;
using Infrastructure.Contexts.Mongo.Shared;
using Infrastructure.Extensions;
using Infrastructure.Mappings.User;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Contexts.Mongo;

public class MongoUserContext(IOptions<MongoDbSettings> settings) : DbMongo(settings)
{
    
    public IMongoCollection<MongoUser> Users;
    
    public IMongoCollection<MongoFollower> Followers;
    public IMongoCollection<MongoFavorite> Favorites;
    public IMongoCollection<MongoRelationship> Relationships;
    
    

    protected override void OnConfigure()
    {
        this.Users = Database.SetCollection(nameof(Users), new UserMap());
    }
}
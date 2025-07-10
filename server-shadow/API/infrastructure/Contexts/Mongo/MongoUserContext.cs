using Domain.Models.Post;
using Domain.Models.User;
using Domain.Options;
using Infrastructure.Contexts.Mongo.Shared;
using Infrastructure.Extensions;
using Infrastructure.Mappings.User;
using Infrastructure.Mappings.Post;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Contexts.Mongo;

public class MongoUserContext(IOptions<MongoDbSettings> settings) : DbMongo(settings)
{
    public IMongoCollection<MongoPost> Posts;
    public IMongoCollection<MongoLike> Likes;
    public IMongoCollection<MongoComment> Comments;
    public IMongoCollection<MongoUser> Users;

    protected override void OnConfigure()
    {
        this.Posts = Database.SetCollection(nameof(Posts), new PostMap());
        this.Users = Database.SetCollection(nameof(Users), new UserMap());
        this.Likes = Database.SetCollection<MongoLike>(nameof(Likes), new LikesMap());
        this.Comments = Database.SetCollection(nameof(Comments), new CommentMap());
    }
}
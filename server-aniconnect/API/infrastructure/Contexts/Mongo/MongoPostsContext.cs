using Domain.Models.Post;
using Domain.Options;
using Infrastructure.Contexts.Mongo.Shared;
using Infrastructure.Extensions;
using Infrastructure.Mappings.Post;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Contexts.Mongo;

public class MongoPostsContext : DbMongo
{
    public IMongoCollection<MongoPost> Posts;
    public IMongoCollection<MongoLike> Likes;
    public IMongoCollection<MongoComment> Comments;
    
    
    public MongoPostsContext(IOptions<MongoDbSettings> settings) : base(settings) { }
    
    
    protected override void OnConfigure()
    {
        this.Posts = Database.SetCollection(nameof(Posts), new PostMap());
        this.Likes = Database.SetCollection<MongoLike>(nameof(Likes), new LikesMap());
        this.Comments = Database.SetCollection(nameof(Comments), new CommentMap());
    }
}
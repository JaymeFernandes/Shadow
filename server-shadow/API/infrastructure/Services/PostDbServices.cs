using Domain.Models.Post;
using Domain.Models.User;
using Domain.ObjectPool;
using Infrastructure.Contexts.Mongo;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class PostDbServices : BackgroundService
{
    private readonly MongoUserContext _mongoContext;
    private readonly MongoPool _mongoPool;

    public PostDbServices(MongoUserContext context, MongoPool mongoPool)
    {
        _mongoContext = context;
        _mongoPool = mongoPool;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_mongoPool.PostsQueue.IsEmpty)
                await WritePosts();

            if (!_mongoPool.LikesQueue.IsEmpty)
                await WriteLikes();

            if (!_mongoPool.CommentsQueue.IsEmpty)
                await WriteComments();

            if(!_mongoPool.UsersQueue.IsEmpty)
                await WriteUsers();


            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }

    private async Task WritePosts()
    {
        List<MongoPost> posts = new();

        while (_mongoPool.PostsQueue.TryDequeue(out var post))
            posts.Add(post);

        var bulkOpsGroups = GetBulkOpsGroups(posts, x => Builders<MongoPost>.Filter.Eq(p => p.Id, x.Id));

        foreach (var batch in bulkOpsGroups)
            await _mongoContext.Posts.BulkWriteAsync(batch);
    }

    private async Task WriteLikes()
    {
        List<MongoLike> likes = new();

        while (_mongoPool.LikesQueue.TryDequeue(out var like))
            likes.Add(like);

        var bulkOpsGroups = GetBulkOpsGroups(likes, x => Builders<MongoLike>.Filter.Eq(p => p.Id, x.Id));

        foreach (var batch in bulkOpsGroups)
            await _mongoContext.Likes.BulkWriteAsync(batch);
    }

    private async Task WriteComments()
    {
        List<MongoComment> comments = new();

        while (_mongoPool.CommentsQueue.TryDequeue(out var comment))
            comments.Add(comment);

        var bulkOpsGroups = GetBulkOpsGroups(comments, x => Builders<MongoComment>.Filter.Eq(y => y.Id, x.Id));

        foreach (var batch in bulkOpsGroups)
            await _mongoContext.Comments.BulkWriteAsync(batch);
    }

    private async Task WriteUsers()
    {
        List<MongoUser> users = new();

        while (_mongoPool.UsersQueue.TryDequeue(out var user))
            users.Add(user);

        var bulkOpsGroups = GetBulkOpsGroups(users, x => Builders<MongoUser>.Filter.Eq(y => y.Id, x.Id));

        foreach (var batch in bulkOpsGroups)
            await _mongoContext.Users.BulkWriteAsync(batch);
    }

    private IEnumerable<IEnumerable<WriteModel<T>>> GetBulkOpsGroups<T>(List<T> values, Func<T, FilterDefinition<T>> filterFunction) where T : class
    {
        return values
            .Select((x, i) =>
            {
                var filter = filterFunction(x);

                return new ReplaceOneModel<T>(filter, x)
                {
                    IsUpsert = true
                };
            })
            .Select((operation, index) => new { operation, index })
            .GroupBy(x => x.index / 1000)
            .Select(g => g.Select(x => x.operation));
    }

}
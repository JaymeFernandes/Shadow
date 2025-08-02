using Domain.Models.Post;
using Infrastructure.Contexts.Mongo.Shared;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Infrastructure.Mappings.Post;

public class LikesMap : IMongoEntityConfiguration<MongoLike>
{
    public void Configure(IMongoCollection<MongoLike> collection)
    {
        var indexKeys = Builders<MongoLike>.IndexKeys;

        var indexes = new List<CreateIndexModel<MongoLike>>
        {
            // Index simples por PostId
            new CreateIndexModel<MongoLike>(
                indexKeys.Ascending(x => x.PostId),
                new CreateIndexOptions { Name = "idx_postId" }
            ),

            // Index simples por CommentId
            new CreateIndexModel<MongoLike>(
                indexKeys.Ascending(x => x.CommentId),
                new CreateIndexOptions { Name = "idx_commentId" }
            ),

            // Index por data de like
            new CreateIndexModel<MongoLike>(
                indexKeys.Descending(x => x.LikedAt),
                new CreateIndexOptions { Name = "idx_likedAt_desc" }
            ),

            // Index simples por UserId
            new CreateIndexModel<MongoLike>(
                indexKeys.Ascending(x => x.UserId),
                new CreateIndexOptions { Name = "idx_userId" }
            ),

            // Index composto: Evitar duplicado de Like no mesmo Post
            new CreateIndexModel<MongoLike>(
                indexKeys.Ascending(x => x.UserId).Ascending(x => x.PostId),
                new CreateIndexOptions
                {
                    Name = "uq_user_post_like",
                    Unique = true,
                    Sparse = true
                }
            ),

            // Index composto: Evitar duplicado de Like no mesmo Comentário
            new CreateIndexModel<MongoLike>(
                indexKeys.Ascending(x => x.UserId).Ascending(x => x.CommentId),
                new CreateIndexOptions
                {
                    Name = "uq_user_comment_like",
                    Unique = true,
                    Sparse = true
                }
            )
        };

        collection.Indexes.CreateMany(indexes);
    }
}
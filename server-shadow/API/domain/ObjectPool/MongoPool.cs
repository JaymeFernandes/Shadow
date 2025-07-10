using Domain.Models.Post;
using Domain.Models.User;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ObjectPool;

public class MongoPool
{
    public ConcurrentQueue<MongoPost> PostsQueue { get; set; } = new();
    public ConcurrentQueue<MongoLike> LikesQueue { get; set; } = new();
    public ConcurrentQueue<MongoComment> CommentsQueue { get; set; } = new();
    public ConcurrentQueue<MongoUser> UsersQueue { get; set; } = new();
}
using Domain.Models.User;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services.Save.Mongo.Shared;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Services.Save.Mongo.Users;

public class UserHandler : MongoBaseHandler<MongoUserContext, MongoUser>
{
    protected override Func<MongoUser, FilterDefinition<MongoUser>> Filter 
        => (x => Builders<MongoUser>.Filter.Eq(u => u.Id, x.Id));

    protected override IMongoCollection<MongoUser> Collection => Context.Users;

    public UserHandler(ILogger<IMongoCollection<MongoUser>> logger, MongoUserContext context) : base(logger, context) { }
}
using Domain.Options;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Interfaces.Save.Mongo;
using Infrastructure.Services;
using Infrastructure.Services.Save.Mongo;
using Infrastructure.Services.Save.Mongo.Posts;
using Infrastructure.Services.Save.Mongo.Users;

namespace Api.Extensions;

public static class MongoDbSetup
{

    public static IServiceCollection AddMongoDbSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("Connections:Mongo"));
        
        services.AddSingleton<MongoUserContext>();
        services.AddSingleton<MongoPostsContext>();

        services.AddSingleton<IDataHandlerMongo, CommentHandler>();
        services.AddSingleton<IDataHandlerMongo, LikeHandler>();
        services.AddSingleton<IDataHandlerMongo, PostsHandler>();

        services.AddSingleton<IDataHandlerMongo, UserHandler>();
        
        services.AddHostedService<MongoDbSyncService>();

        return services;
    }
}
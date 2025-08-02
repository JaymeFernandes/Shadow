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

        // Comentários
        services.AddSingleton<CommentHandler>();
        services.AddSingleton<IDataHandlerMongo>(sp => sp.GetRequiredService<CommentHandler>());

        // Likes
        services.AddSingleton<LikeHandler>();
        services.AddSingleton<IDataHandlerMongo>(sp => sp.GetRequiredService<LikeHandler>());

        // Posts
        services.AddSingleton<PostsHandler>();
        services.AddSingleton<IDataHandlerMongo>(sp => sp.GetRequiredService<PostsHandler>());

        // Usuários
        services.AddSingleton<UserHandler>();
        services.AddSingleton<IDataHandlerMongo>(sp => sp.GetRequiredService<UserHandler>());

        services.AddHostedService<MongoDbSyncService>();

        return services;
    }
}
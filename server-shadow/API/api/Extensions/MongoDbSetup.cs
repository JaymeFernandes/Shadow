using Domain.Options;
using Infrastructure.Contexts.Mongo;
using Infrastructure.Services;

namespace Api.Extensions;

public static class MongoDbSetup
{

    public static IServiceCollection AddMongoDbSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("Connections:Mongo"));
        services.AddSingleton<MongoUserContext>();

        services.AddHostedService<PostDbServices>(); //in development

        return services;
    }
}
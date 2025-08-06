using Meilisearch;

namespace Api.Extensions;

public static class MeilisearchSetup
{
    public static IServiceCollection AddMeilisearchSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(serviceprovider =>
        {
            var host = configuration["MeiliSearch:Host"];
            var key = configuration["MeiliSearch:Key"];
    
            if(string.IsNullOrEmpty(host) || string.IsNullOrEmpty(key))
                throw new Exception("Host and key are required");
    
            return new MeilisearchClient(host, key);
        });

        return services;
    }
}
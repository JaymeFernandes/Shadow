using Domain.ObjectPool;
using Infrastructure.Contexts.Catalog;
using Infrastructure.Contexts.Identity;
using Infrastructure.Contexts.Works;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions;

public static class EfCoreSetup
{
    public static IServiceCollection AddEfCoreSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityContext>(x =>
        {
            x.UseNpgsql(configuration["Connections:PostgreSql:Identity"]);
            x.UseOpenIddict();
        });

        services.AddDbContext<CatalogAppContext>(x =>
        {
            x.UseNpgsql(configuration["Connections:PostgreSql:Catalog"]);
        });

        services.AddDbContext<WorkAppContext>(x =>
        {
            x.UseNpgsql(configuration["Connections:PostgreSql:Work"]);
        });

            

        services.AddSingleton<MongoPool>();

        return services;
    }

}
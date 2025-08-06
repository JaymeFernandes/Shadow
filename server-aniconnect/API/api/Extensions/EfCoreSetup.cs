using Infrastructure.Contexts.Content;
using Infrastructure.Contexts.Identity;
using Infrastructure.Services.Save.EfCore;
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

        services.AddDbContext<ContentAppContext>(x =>
        {
            x.UseNpgsql(configuration["Connections:PostgreSql:Content"]);
        });
        
        // services.AddSingleton<IDataHandlerEfCore, WorkHandler>(x => x.GetService<WorkHandler>());
        
        
        // services.AddScoped<IWorkRepository, WorkRepository>();

        
        
        services.AddHostedService<EfCoreDbSyncService>();

        return services;
    }

}
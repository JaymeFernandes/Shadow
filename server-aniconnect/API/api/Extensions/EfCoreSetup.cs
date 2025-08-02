using Infrastructure.Contexts.Catalog;
using Infrastructure.Contexts.Identity;
using Infrastructure.Contexts.Works;
using Infrastructure.Interfaces.Save.EfCore;
using Infrastructure.Interfaces.Work;
using Infrastructure.Repositories;
using Infrastructure.Services.Save.EfCore;
using Infrastructure.Services.Save.EfCore.Work;
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

        services.AddSingleton<AuthorsHandler>();
        services.AddSingleton<IDataHandlerEfCore, AuthorsHandler>(x => x.GetService<AuthorsHandler>());

        services.AddSingleton<WorkCategoryHandler>();
        services.AddSingleton<IDataHandlerEfCore, WorkCategoryHandler>(x => x.GetService<WorkCategoryHandler>());

        services.AddSingleton<ChaptersHandler>();
        services.AddSingleton<IDataHandlerEfCore, ChaptersHandler>(x => x.GetService<ChaptersHandler>());

        services.AddSingleton<NamesHandler>();
        services.AddSingleton<IDataHandlerEfCore, NamesHandler>(x => x.GetService<NamesHandler>());

        services.AddSingleton<WorkTagHandler>();
        services.AddSingleton<IDataHandlerEfCore, WorkTagHandler>(x => x.GetService<WorkTagHandler>());
        
        
        services.AddSingleton<WorkHandler>();
        services.AddSingleton<IDataHandlerEfCore, WorkHandler>(x => x.GetService<WorkHandler>());
        
        
        services.AddScoped<IWorkRepository, WorkRepository>();

        
        
        services.AddHostedService<EfCoreDbSyncService>();

        return services;
    }

}
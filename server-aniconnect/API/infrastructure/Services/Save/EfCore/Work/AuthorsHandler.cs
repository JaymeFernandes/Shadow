using Domain.Models.Works;
using Infrastructure.Contexts.Works;
using Infrastructure.Services.Save.EfCore.Shared;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Work;

public class AuthorsHandler : EfCoreHandlerBase<WorkAppContext, Author>
{
    

    public string Name 
        => "AuthorsHandler";
    
    public AuthorsHandler(IServiceProvider serviceProvider, ILogger<WorkAppContext> logger) : base(serviceProvider, logger)
    {
    }
}
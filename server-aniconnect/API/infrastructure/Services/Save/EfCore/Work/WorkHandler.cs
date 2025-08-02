using Infrastructure.Contexts.Works;
using Infrastructure.Services.Save.EfCore.Shared;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Work;

public class WorkHandler : EfCoreHandlerBase<WorkAppContext, Domain.Models.Works.Work>
{
    public string Name 
        => "WorkHandler";
    

    public WorkHandler(IServiceProvider serviceProvider, ILogger<WorkAppContext> logger) : base(serviceProvider, logger)
    {
    }
}
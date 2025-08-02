using Domain.Models.Works;
using Infrastructure.Contexts.Works;
using Infrastructure.Services.Save.EfCore.Shared;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Work;

public class WorkTagHandler : EfCoreHandlerBase<WorkAppContext, Tag>
{
    public override string Name 
        => "WorkTagHandler";

    public WorkTagHandler(IServiceProvider serviceProvider, ILogger<WorkAppContext> logger) : base(serviceProvider, logger)
    {
    }
}
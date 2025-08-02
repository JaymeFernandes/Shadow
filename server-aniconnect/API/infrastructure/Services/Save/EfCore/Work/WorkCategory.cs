using Domain.Models.Works;
using Infrastructure.Contexts.Works;
using Infrastructure.Services.Save.EfCore.Shared;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Work;

public class WorkCategoryHandler : EfCoreHandlerBase<WorkAppContext, Category>
{
    public override string Name => "CategoryHandler";

    public WorkCategoryHandler(IServiceProvider serviceProvider, ILogger<WorkAppContext> logger) : base(serviceProvider, logger)
    {
    }
}
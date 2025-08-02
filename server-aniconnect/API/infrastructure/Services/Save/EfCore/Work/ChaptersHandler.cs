using Domain.Models.Works;
using Infrastructure.Contexts.Works;
using Infrastructure.Services.Save.EfCore.Shared;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Work;

public class ChaptersHandler : EfCoreHandlerBase<WorkAppContext, Chapter>
{
    public string Name 
        => "ChaptersHandler";

    public ChaptersHandler(IServiceProvider serviceProvider, ILogger<WorkAppContext> logger) : base(serviceProvider, logger)
    {
    }
}
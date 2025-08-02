using Domain.Models.Works;
using Infrastructure.Contexts.Works;
using Infrastructure.Services.Save.EfCore.Shared;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Save.EfCore.Work;

public class NamesHandler : EfCoreHandlerBase<WorkAppContext, Names>
{
    public string Name 
        => "NamesHandler";

    public NamesHandler(IServiceProvider serviceProvider, ILogger<WorkAppContext> logger) : base(serviceProvider, logger)
    {
    }
}
namespace Infrastructure.Interfaces.Save.Shared;

public interface IDataQueueHandler
{
    string Name { get; }
    
    Task ProcessAsync(CancellationToken token);
    
    bool HasPendingItems { get; }
}
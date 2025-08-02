using System.Collections.Concurrent;

namespace Infrastructure.Extensions;

public static class ConcurrentQueueExtensions
{
    public static List<T> DrainToList<T>(this ConcurrentQueue<T> queue, CancellationToken? token = null)
    {
        var list = new List<T>();

        while (queue.TryDequeue(out var item))
        {
            if(token != null && token.Value.IsCancellationRequested)
                break;
            
            list.Add(item);
        }
        
        return list;
    }
}
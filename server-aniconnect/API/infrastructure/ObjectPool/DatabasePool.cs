using System.Threading.Channels;

namespace Infrastructure.ObjectPool;

public abstract class Database<TContext, T> where T : class
{
    protected TContext Context { get; set; }
    
    private static readonly BoundedChannelOptions _options = new(2000)
    {
        FullMode = BoundedChannelFullMode.Wait
    };
    
    private readonly Channel<T> _channel = Channel.CreateBounded<T>(_options);
    protected ChannelReader<T> Reader => _channel.Reader;
    private ChannelWriter<T> Writer => _channel.Writer;
    

    public async Task Add(T entity)
    {
        await _channel.Writer.WriteAsync(entity);
        
        Console.WriteLine();
    }
    

    public async Task AddRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
            await _channel.Writer.WriteAsync(entity);
    }
}
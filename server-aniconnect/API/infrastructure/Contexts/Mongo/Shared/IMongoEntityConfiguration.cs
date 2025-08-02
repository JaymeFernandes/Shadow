using MongoDB.Driver;

namespace Infrastructure.Contexts.Mongo.Shared;

public interface IMongoEntityConfiguration<T>
{
    void Configure(IMongoCollection<T> collection);
}
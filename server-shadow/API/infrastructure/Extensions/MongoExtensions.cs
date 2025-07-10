using Infrastructure.Attributes;
using Infrastructure.Contexts.Mongo.Shared;
using MongoDB.Driver;
using System.Reflection;

namespace Infrastructure.Extensions;

public static class MongoExtensions
{
    public static void ApplyConfiguration<T>(this IMongoCollection<T> context, IMongoEntityConfiguration<T> configuration) where T : class
        => configuration.Configure(context);

    public static IMongoCollection<T> SetCollection<T>(this IMongoDatabase database) where T : class
    {
        var name = GetCollectionName<T>();

        return database.GetCollection<T>(name);
    }

    public static IMongoCollection<T> SetCollection<T>(this IMongoDatabase database, string name) where T : class
        => database.GetCollection<T>(name);

    public static IMongoCollection<T> SetCollection<T>(this IMongoDatabase database, string name, IMongoEntityConfiguration<T> configuration) where T : class
    {
        var collection = database.GetCollection<T>(name.ToLower());

        configuration.Configure(collection);

        return collection;
    }

    public static IMongoCollection<T> SetCollection<T>(this IMongoDatabase database, IMongoEntityConfiguration<T> configuration) where T : class
    {
        var name = GetCollectionName<T>();

        var collection = database.GetCollection<T>(name.ToLower());

        configuration.Configure(collection);

        return collection;
    }

    private static string GetCollectionName<T>()
    {
        var attribute = typeof(T).GetCustomAttribute<MongoCollectionNameAttribute>();

        if (attribute != null && string.IsNullOrWhiteSpace(attribute.Name))
            throw new InvalidOperationException($"Collection name for {typeof(T).Name} is empty in attribute.");

        return attribute?.Name ?? $"collection_{typeof(T).Name}";
    }
}
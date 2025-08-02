using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class EfCoreExtensions
{
    public static async Task BulkSaveWithTransactionAsync<T>(this DbContext context, List<T> entities,
        CancellationToken token) where T : class
    {
        if(!entities.Any())
            return;

        using var transaction = await context.Database.BeginTransactionAsync(token);
        
        
        await context.BulkInsertOrUpdateAsync(entities, x =>
        {
            x.SetOutputIdentity = true;
        }, cancellationToken:token);
        
        await transaction.CommitAsync(token);
    }
}
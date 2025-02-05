// packages
using Microsoft.EntityFrameworkCore;

namespace server.src.Persistence.Common.Extensions;

public static class DbContextExtensions
{
    public static string? GetDbSetName<TContext, TEntity>(this TContext context, DbSet<TEntity> dbSet)
        where TContext : DbContext
        where TEntity : class
    {
        var propertyInfo = context.GetType().GetProperties()
            .SingleOrDefault(p => p.PropertyType == typeof(DbSet<TEntity>) && p.GetValue(context) == dbSet);

        return propertyInfo?.Name;
    }
}
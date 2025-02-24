// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Common.Contexts;

namespace server.src.Api.Helpers;

public static class DatabaseMigrationHelper
{
    public static async Task ApplyMigrationsAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        try
        {
            var context = serviceProvider.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            // Use a general type for logging context
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}

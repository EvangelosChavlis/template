// packages
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Persistence.Contexts;

namespace server.test.Api.Integration.Tests.TestHelpers;

public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    private readonly string _dbName = "TestDatabase";
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's default DbContext configuration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Add an in-memory database for testing
            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName); 
            });

            // Build the service provider and ensure the database is cleared and seeded for each test
            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.EnsureCreated(); 
            }
        });
    }

    // Expose DataContext for querying in tests
    public DataContext GetDataContext()
    {
        var serviceScope = Services.CreateScope();
        return serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    }
}

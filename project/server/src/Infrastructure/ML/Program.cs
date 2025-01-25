using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.ML;
using server.src.Application.Interfaces.Weather;
using server.src.Application.Services.Weather;
using server.src.Persistence.Contexts;  // Add your DataContext namespace
using server.src.Persistence.Interfaces; // For ICommonRepository
using server.src.Persistence.Repositories; // For CommonRepository
using Microsoft.EntityFrameworkCore; // Needed for AddDbContext
using Microsoft.Extensions.Caching.Memory; // Add MemoryCache support

class Program
{
    private readonly IForecastsService _forecastsService;

    // Constructor for Dependency Injection
    public Program(IForecastsService forecastsService)
    {
        _forecastsService = forecastsService;
    }

    // Static Main with async
    public static async Task Main(string[] args)
    {
        // Build the configuration from appsettings.json or environment variables
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Set up dependency injection
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)  // Add IConfiguration
            .AddScoped<IForecastsService, ForecastsService>() // Register ForecastsService
            .AddScoped<ICommonRepository, CommonRepository>() // Register ICommonRepository and its implementation
            .AddScoped<Program>() // Add the Program class itself
            // Register the DataContext explicitly
            .AddDbContext<DataContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))) // Use PostgreSQL connection string
            .AddMemoryCache() // Add IMemoryCache
            .BuildServiceProvider();

        // Resolve the Program instance
        var program = serviceProvider.GetRequiredService<Program>();
        await program.Run();
    }

    public async Task Run()
    {
        var token = CancellationToken.None; // Replace with actual token if available
        var data = await _forecastsService.GetForecastsStatsService(token); // Task<ItemResponse<List<StatItemForecastDto>>>

        // Check if data is not null and has a 'Data' property
        if (data != null && data.Data != null)
        {
            Console.WriteLine("Forecast Stats Retrieved:");

            // Loop through the forecast stats and print them out
            foreach (var forecast in data.Data) // 'Data' should match the actual property in 'ItemResponse'
            {
                // Print the properties from StatItemForecastDto
                Console.WriteLine($"Date: {forecast.Date}, Temperature: {forecast.TemperatureC}°C");
            }
        }
        else
        {
            Console.WriteLine("No forecast data available.");
        }

        // ML.NET setup
        var mlContext = new MLContext();

        // If you want to load data for further ML tasks, ensure the file and schema exist.
        // Example: var dataView = mlContext.Data.LoadFromTextFile<TemperatureData>("temperature_data.csv", hasHeader: true, separatorChar: ',');

        // No need to print the raw data object anymore
    }
}

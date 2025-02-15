// packages
using Microsoft.Extensions.Options;

// source
using server.src.Api.Extensions;
using server.src.Domain.Common.Models;

namespace server.src.Api;

public static class DI
{
    public static IServiceCollection AddApi(this IServiceCollection services, ILoggingBuilder logging, 
        IWebHostBuilder webHostBuilder, IHostEnvironment environment)
    {
        // Configure Kestrel for production
        webHostBuilder.ConfigureKestrel((context, options) =>
        {
            if (environment.IsProduction())
            {
                options.ListenAnyIP(5000);
            }
        });

        // Build configuration with appsettings.json and environment-specific settings
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var builtConfiguration = configurationBuilder.Build();

        // Swagger Configuration
        services.AddCustomSwagger();

        // Memory Cache for Rate Limiting
        services.AddMemoryCache();

        // Rate Limiting Configuration
        services.ConfigureRateLimitingOptions();

        // Add HttpContextAccessor for middleware and services
        services.AddHttpContextAccessor();

        // CORS Configuration
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", builder =>
            {
                builder.WithOrigins(builtConfiguration["AllowedOrigins"] ?? "http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.Configure<JwtSettings>(builtConfiguration.GetSection("Jwt"));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtSettings>>().Value);

        logging.ClearProviders();
        logging.AddConsole();

        return services;
    }
}
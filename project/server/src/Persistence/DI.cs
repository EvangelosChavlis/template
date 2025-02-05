// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Persistence.Auth;
using server.src.Persistence.Common.Interfaces;
using server.src.Persistence.Common.Repositories;
using server.src.Persistence.Geography;
using server.src.Persistence.Metrics;
using server.src.Persistence.Weather;

namespace server.src.Persistence;

public static class DI
{
    private static readonly string _connectionString = "DefaultConnection";
    private static readonly string _archiveConnectionString = "ArchiveConnection";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<DataContext>(options =>
        //     options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        // services.AddDbContext<ArchiveContext>(options =>
        //     options.UseNpgsql(configuration.GetConnectionString(_archiveConnectionString)));

        services.AddDbContext<AuthContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        services.AddDbContext<WeatherContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        services.AddDbContext<MetricsContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        services.AddDbContext<GeographyContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommonRepository, CommonRepository>();

        return services;
    }
}
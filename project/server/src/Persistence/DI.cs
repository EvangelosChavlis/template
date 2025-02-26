// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Persistence.Common.Contexts;
using server.src.Persistence.Common.Interfaces;
using server.src.Persistence.Common.Repositories;

namespace server.src.Persistence;

public static class DI
{
    private static readonly string _connectionString = "DataConnection";
    private static readonly string _archiveConnectionString = "ArchiveConnection";

    public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        services.AddDbContext<ArchiveContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_archiveConnectionString)));


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommonRepository, CommonRepository>();

        return services;
    }
}
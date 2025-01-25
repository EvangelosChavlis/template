// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;
using server.src.Persistence.Repositories;

namespace server.src.Persistence;

public static class DependencyInjection
{
    private static readonly string _connectionString = "DefaultConnection";

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(_connectionString)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICommonRepository, CommonRepository>();
        
        
        return services;
    }
}
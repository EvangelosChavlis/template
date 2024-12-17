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
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICommonRepository, CommonRepository>();
        
        
        return services;
    }
}
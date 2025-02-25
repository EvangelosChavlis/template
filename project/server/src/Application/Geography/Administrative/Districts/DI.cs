// packages
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Geography.Administrative.Districts.Interfaces;
using server.src.Application.Geography.Administrative.Districts.Services;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Services;

namespace server.src.Application.Geography.Administrative.Districts;

public static class DI
{
    public static IServiceCollection RegisterDistricts(this IServiceCollection services)
    {       
        var assembly = Assembly.GetExecutingAssembly();
        
        // Register all IRequestHandler<TRequest, TResponse> implementations dynamically
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var interfaceType = handlerType.GetInterfaces().First(i => 
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            services.AddScoped(interfaceType, handlerType);
        }

        // Register RequestExecutor
        services.AddSingleton<RequestExecutor>();

        // Register DistrictCommands and DistrictQueries
        services.AddTransient<IDistrictCommands, DistrictCommands>();
        services.AddTransient<IDistrictQueries, DistrictQueries>();

        return services;
    }
}

// packages
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Services;
using server.src.Application.Weather.Collections.Observations.Interfaces;
using server.src.Application.Weather.Collections.Observations.Services;

namespace server.src.Application.Weather.Collections.Observations;

public static class DI
{
    public static IServiceCollection RegisterObservations(this IServiceCollection services)
    {
        services.AddMemoryCache();
                
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

        // Register ObservationCommands and RoleQueries
        services.AddTransient<IObservationCommands, ObservationCommands>();
        services.AddTransient<IObservationQueries, ObservationQueries>();

        return services;
    }
}

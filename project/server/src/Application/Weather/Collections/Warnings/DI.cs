// packages
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Weather.Collections.Warnings.Interfaces;
using server.src.Application.Weather.Collections.Warnings.Services;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Services;

namespace server.src.Application.Weather.Collections.Warnings;

public static class DI
{
    public static IServiceCollection RegisterWarnings(this IServiceCollection services)
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

        // Register WarningCommands and WarningQueries
        services.AddTransient<IWarningCommands, WarningCommands>();
        services.AddTransient<IWarningQueries, WarningQueries>();

        return services;
    }
}

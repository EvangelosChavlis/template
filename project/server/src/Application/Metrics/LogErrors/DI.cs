// packages
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Services;
using server.src.Application.Metrics.Errors.Services;
using server.src.Application.Metrics.LogErrors.Interfaces;

namespace server.src.Application.Metrics.Errors;

public static class DI
{
    public static IServiceCollection AddLogErrors(this IServiceCollection services)
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

        // Register RoleCommands and RoleQueries
        services.AddTransient<ILogErrorQueries, LogErrorQueries>();

        return services;
    }
}

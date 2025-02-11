// packages
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Auth.Roles.Services;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Services;

namespace server.src.Application.Auth.Roles;

public static class DI
{
    public static IServiceCollection AddRoles(this IServiceCollection services)
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
        services.AddTransient<IRoleCommands, RoleCommands>();
        services.AddTransient<IRoleQueries, RoleQueries>();

        return services;
    }
}

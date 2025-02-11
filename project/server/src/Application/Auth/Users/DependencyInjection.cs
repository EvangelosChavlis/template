// packages
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Auth.Users.Services;
using server.src.Application.Common.Interfaces;

namespace server.src.Application.Auth.Users;

public static class DependencyInjection
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && 
                    i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        foreach (var handlerType in handlerTypes)
        {
            var interfaceType = handlerType.GetInterfaces().First(i => 
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

            services.AddScoped(interfaceType, handlerType);
        }

        services.AddTransient<IUserCommands, UserCommands>();
        
        services.AddSingleton(sp => sp);

        return services;
    }
}

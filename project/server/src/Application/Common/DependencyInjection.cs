// packages
using System.Reflection;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Commands;
using server.src.Application.Common.Events;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Queries;
using server.src.Application.Common.Services;

namespace server.src.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();

        // Register all handlers dynamically
        RegisterNotificationHandlers(services, Assembly.GetExecutingAssembly());

        // // register query handlers
        // services.AddScoped<IRequestHandler<DecryptSensitiveDataQuery, object>, DecryptSensitiveDataHandler>();
        // services.AddScoped<IRequestHandler<EncryptSensitiveDataQuery, string>, EncryptSensitiveDataHandler>();
        // services.AddScoped<IRequestHandler<GeneratePasswordQuery, string>, GeneratePasswordHandler>();
        // services.AddScoped<IRequestHandler<GetPrincipalFromExpiredTokenQuery, ClaimsPrincipal>, GetPrincipalFromExpiredTokenHandler>();
        // services.AddScoped<IRequestHandler<HashPasswordQuery, string>, HashPasswordHandler>();
        // services.AddScoped<IRequestHandler<VerifyPasswordQuery, bool>, VerifyPasswordHandler>();

        // // register queries
        // services.AddScoped<ICommonQueries, CommonQueries>();

        // // register command handlers
        // services.AddScoped<IRequestHandler<GenerateJwtTokenCommand, Response<string>>, GenerateJwtTokenHandler>();

        // register commands
        services.AddScoped<ICommonCommands, CommonCommands>();

        return services;
    }

    private static void RegisterNotificationHandlers(IServiceCollection services, Assembly assembly)
    {
        var handlerType = typeof(INotificationHandler<>);

        // Get all types that implement INotificationHandler<T>
        var handlerTypes = assembly.GetTypes()
            .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType))
            .ToList();

        foreach (var type in handlerTypes)
        {
            var interfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType);

            foreach (var @interface in interfaces)
            {
                services.AddScoped(@interface, type);
            }
        }
    }
}

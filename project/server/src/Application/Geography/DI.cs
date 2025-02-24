using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Geography.Administrative;
using server.src.Application.Geography.Natural;

namespace server.src.Application.Geography;

public static class DI
{
    public static IServiceCollection RegisterGeography(this IServiceCollection services)
    {
        services.RegisterAdministrative();
        services.RegisterNatural();

        return services;
    }
}
// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Geography.Administrative.Continents;
using server.src.Application.Geography.Administrative.Countries;

namespace server.src.Application.Geography.Administrative;

public static class DI
{
    public static IServiceCollection RegisterAdministrative(this IServiceCollection services)
    {
        services.RegisterContinents();
        services.RegisterCountries();

        return services;
    }
}
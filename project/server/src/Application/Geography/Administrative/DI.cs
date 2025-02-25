// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Geography.Administrative.Continents;
using server.src.Application.Geography.Administrative.Countries;
using server.src.Application.Geography.Administrative.Districts;
using server.src.Application.Geography.Administrative.Municipalities;
using server.src.Application.Geography.Administrative.Neighborhoods;
using server.src.Application.Geography.Administrative.Regions;
using server.src.Application.Geography.Administrative.States;

namespace server.src.Application.Geography.Administrative;

public static class DI
{
    public static IServiceCollection RegisterAdministrative(this IServiceCollection services)
    {
        services.RegisterContinents();
        services.RegisterCountries();
        services.RegisterStates();
        services.RegisterRegions();
        services.RegisterMunicipalities();
        services.RegisterDistricts();
        services.RegisterNeighborhoods();

        return services;
    }
}
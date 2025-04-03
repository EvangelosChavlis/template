using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Geography.Natural.ClimateZones;
using server.src.Application.Geography.Natural.Locations;
using server.src.Application.Geography.Natural.NaturalFeatures;
using server.src.Application.Geography.Natural.SurfaceTypes;
using server.src.Application.Geography.Natural.Timezones;

namespace server.src.Application.Geography.Natural;

public static class DI
{
    public static IServiceCollection RegisterNatural(this IServiceCollection services)
    {
        services.RegisterClimateZones();
        services.RegisterLocations();
        services.RegisterNaturalFeatures();
        services.RegisterSurfaceTypes();
        services.RegisterTimezones();

        return services;
    }
}
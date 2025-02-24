using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Geography.Natural.ClimateZones;
using server.src.Application.Geography.Natural.Locations;
using server.src.Application.Geography.Natural.TerrainTypes;
using server.src.Application.Geography.Natural.Timezones;

namespace server.src.Application.Geography.Natural;

public static class DI
{
    public static IServiceCollection RegisterNatural(this IServiceCollection services)
    {
        services.RegisterClimateZones();
        services.RegisterLocations();
        services.RegisterTimezones();
        services.RegisterTerrainTypes();

        return services;
    }
}
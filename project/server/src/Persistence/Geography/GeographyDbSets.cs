using Microsoft.EntityFrameworkCore;
using server.src.Domain.Geography.ClimateZones.Models;
using server.src.Domain.Geography.Locations.Models;
using server.src.Domain.Geography.TerrainTypes.Models;
using server.src.Domain.Geography.Timezones.Models;

namespace server.src.Persistence.Geography;

public class GeographyDbSets
{
    public DbSet<ClimateZone> ClimateZones { get; private set; }
    public DbSet<Location> Locations { get; private set; }
    public DbSet<TerrainType> TerrainTypes { get; private set; }
    public DbSet<Timezone> Timezones { get; private set; }

    public GeographyDbSets(DbContext context)
    {
        ClimateZones = context.Set<ClimateZone>();
        Locations = context.Set<Location>();
        TerrainTypes = context.Set<TerrainType>();
        Timezones = context.Set<Timezone>();
    }
}
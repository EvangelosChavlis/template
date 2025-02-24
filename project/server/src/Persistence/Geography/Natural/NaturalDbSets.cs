// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Domain.Geography.Natural.TerrainTypes.Models;
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Persistence.Geography.Natural;

public class NaturalDbSets
{
    public DbSet<Location> Locations { get; private set; }
    public DbSet<NaturalFeature> NaturalFeatures { get; private set; }
    public DbSet<ClimateZone> ClimateZones { get; private set; }
    public DbSet<TerrainType> TerrainTypes { get; private set; }
    public DbSet<Timezone> Timezones { get; private set; }

    public NaturalDbSets(DbContext context)
    {
        Locations = context.Set<Location>();
        NaturalFeatures = context.Set<NaturalFeature>();
        ClimateZones = context.Set<ClimateZone>();
        TerrainTypes = context.Set<TerrainType>();
        Timezones = context.Set<Timezone>();
    }
}
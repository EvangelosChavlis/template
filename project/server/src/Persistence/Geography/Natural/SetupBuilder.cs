// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Geography.Natural.ClimateZones;
using server.src.Persistence.Geography.Natural.Locations;
using server.src.Persistence.Geography.Natural.TerrainTypes;
using server.src.Persistence.Geography.Natural.Timezones;

namespace server.src.Persistence.Geography.Natural;

public static class SetupBuilder
{
    private readonly static string _naturalSchema = "geography_natural";

    public static void AddNatural(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new ClimateZoneConfiguration("ClimateZone", _naturalSchema));
        modelBuilder.ApplyConfiguration(new LocationConfiguration("Locations", _naturalSchema));
        modelBuilder.ApplyConfiguration(new TerrainTypeConfiguration("TerrainTypes", _naturalSchema));
        modelBuilder.ApplyConfiguration(new TimezoneConfiguration("TimeZones", _naturalSchema));
        #endregion
    }
}
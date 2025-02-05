// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Configurations.Geography.Timezones;
using server.src.Persistence.Geography.ClimateZones;
using server.src.Persistence.Geography.Locations;
using server.src.Persistence.Geography.TerrainTypes;

namespace server.src.Persistence.Geography;

public static class DI
{
    private readonly static string _geographySchema = "geography";

    public static void AddGeography(this ModelBuilder modelBuilder)
    {
        #region Geography Configuration
        modelBuilder.ApplyConfiguration(new ClimateZoneConfiguration("ClimateZone", _geographySchema));
        modelBuilder.ApplyConfiguration(new LocationConfiguration("Locations", _geographySchema));
        modelBuilder.ApplyConfiguration(new TerrainTypeConfiguration("TerrainTypes", _geographySchema));
        modelBuilder.ApplyConfiguration(new TimezoneConfiguration("TimeZones", _geographySchema));
        #endregion
    }
}
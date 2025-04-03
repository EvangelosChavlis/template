// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Geography.Natural.ClimateZones;
using server.src.Persistence.Geography.Natural.Locations;
using server.src.Persistence.Geography.Natural.NaturalFeatures;
using server.src.Persistence.Geography.Natural.SurfaceTypes;
using server.src.Persistence.Geography.Natural.Timezones;

namespace server.src.Persistence.Geography.Natural;

public static class SetupBuilder
{
    private readonly static string _naturalSchema = "geography_natural";

    public static void SetupNatural(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new ClimateZoneConfiguration("ClimateZones", _naturalSchema));
        modelBuilder.ApplyConfiguration(new LocationConfiguration("Locations", _naturalSchema));
        modelBuilder.ApplyConfiguration(new NaturalFeatureConfiguration("NaturalFeatures", _naturalSchema));
        modelBuilder.ApplyConfiguration(new SurfaceTypeConfiguration("SurfaceTypes", _naturalSchema));
        modelBuilder.ApplyConfiguration(new TimezoneConfiguration("Timezones", _naturalSchema));
        #endregion

        #region Indexes
        modelBuilder.ApplyConfiguration(new ClimateZoneIndexes());
        modelBuilder.ApplyConfiguration(new LocationIndexes());
        modelBuilder.ApplyConfiguration(new NaturalFeatureIndexes());
        modelBuilder.ApplyConfiguration(new SurfaceTypeIndexes());
        modelBuilder.ApplyConfiguration(new TimezoneIndexes());
        #endregion
    }
}
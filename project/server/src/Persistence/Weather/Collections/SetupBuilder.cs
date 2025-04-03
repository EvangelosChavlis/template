// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Weather.Collections.Forecasts;
using server.src.Persistence.Weather.Collections.MoonPhases;
using server.src.Persistence.Weather.Collections.Observations;
using server.src.Persistence.Weather.Collections.Warnings;

namespace server.src.Persistence.Weather.Collections;

public static class SetupBuilder
{
    private readonly static string _collectionsSchema = "weather_collections";

    public static void SetupCollections(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new ForecastConfiguration("Forecasts", _collectionsSchema));
        modelBuilder.ApplyConfiguration(new MoonPhaseConfiguration("MoonPhases", _collectionsSchema));
        modelBuilder.ApplyConfiguration(new ObservationConfiguration("Observations", _collectionsSchema));
        modelBuilder.ApplyConfiguration(new WarningConfiguration("Warnings", _collectionsSchema));
        #endregion

        #region Indexes
        modelBuilder.ApplyConfiguration(new ForecastIndexes());
        modelBuilder.ApplyConfiguration(new MoonPhaseIndexes());
        modelBuilder.ApplyConfiguration(new ObservationIndexes());
        modelBuilder.ApplyConfiguration(new WarningIndexes());
        #endregion
    }
}
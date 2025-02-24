// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Weather.Forecasts;
using server.src.Persistence.Weather.MoonPhases;
using server.src.Persistence.Weather.Observations;
using server.src.Persistence.Weather.Warnings;

namespace server.src.Persistence.Weather;

public static class SetupBuilder
{
    private readonly static string _weatherSchema = "weather";

    public static void AddWeather(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new ForecastConfiguration("Forecasts", _weatherSchema));
        modelBuilder.ApplyConfiguration(new MoonPhaseConfiguration("MoonPhases", _weatherSchema));
        modelBuilder.ApplyConfiguration(new ObservationConfiguration("Observations", _weatherSchema));
        modelBuilder.ApplyConfiguration(new WarningConfiguration("Warnings", _weatherSchema));
        #endregion

        #region Indexes
        modelBuilder.ApplyConfiguration(new ForecastIndexes());
        modelBuilder.ApplyConfiguration(new MoonPhaseIndexes());
        modelBuilder.ApplyConfiguration(new ObservationIndexes());
        modelBuilder.ApplyConfiguration(new WarningIndexes());
        #endregion
    }
}
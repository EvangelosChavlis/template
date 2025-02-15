using Microsoft.EntityFrameworkCore;
using server.src.Persistence.Weather.Forecasts;
using server.src.Persistence.Weather.MoonPhases.Models;
using server.src.Persistence.Weather.Observations;
using server.src.Persistence.Weather.Warnings;

namespace server.src.Persistence.Weather;

public static class DI
{
    private readonly static string _weatherSchema = "weather";

    public static void AddWeather(this ModelBuilder modelBuilder)
    {
        #region Weather Configuration
        modelBuilder.ApplyConfiguration(new ForecastConfiguration("Forecasts", _weatherSchema));
        modelBuilder.ApplyConfiguration(new MoonPhaseConfiguration("MoonPhases", _weatherSchema));
        modelBuilder.ApplyConfiguration(new ObservationConfiguration("Observations", _weatherSchema));
        modelBuilder.ApplyConfiguration(new WarningConfiguration("Warnings", _weatherSchema));
        #endregion
    }
}
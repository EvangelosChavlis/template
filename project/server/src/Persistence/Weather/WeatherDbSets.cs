using Microsoft.EntityFrameworkCore;
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Persistence.Weather;

public class WeatherDbSets
{
    public DbSet<Forecast> Forecasts { get; private set; }
    public DbSet<MoonPhase> MoonPhases { get; private set; }
    public DbSet<Warning> Warnings { get; private set; }

    public WeatherDbSets(DbContext context)
    {
        Forecasts = context.Set<Forecast>();
        MoonPhases = context.Set<MoonPhase>();
        Warnings = context.Set<Warning>();
    }
}
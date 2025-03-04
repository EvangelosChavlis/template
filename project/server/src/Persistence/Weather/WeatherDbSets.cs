// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Weather.Notifications.Models;
using server.src.Domain.Weather.Observations.Models;
using server.src.Domain.Weather.Reports.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Persistence.Weather;

public class WeatherDbSets
{
    public DbSet<Forecast> Forecasts { get; private set; }
    public DbSet<MoonPhase> MoonPhases { get; private set; }
    public DbSet<Notification> Notifications { get; private set; }
    public DbSet<Observation> Observations { get; private set; }
    public DbSet<Report> Reports { get; private set; }
    public DbSet<Warning> Warnings { get; private set; }

    public WeatherDbSets(DbContext context)
    {
        Forecasts = context.Set<Forecast>();
        MoonPhases = context.Set<MoonPhase>();
        Observations = context.Set<Observation>();
        Warnings = context.Set<Warning>();
    }
}
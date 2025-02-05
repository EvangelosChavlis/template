// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Weather.Forecasts.Models;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Domain.Weather.Warnings.Models;

namespace server.src.Persistence.Weather;

public class WeatherContext : DbContext
{
    public DbSet<Forecast> Forecasts { get; set; }
    public DbSet<MoonPhase> MoonPhases { get; set; }
    public DbSet<Warning> Warnings { get; set; }

    public WeatherContext(DbContextOptions<WeatherContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddWeather();
    }
}

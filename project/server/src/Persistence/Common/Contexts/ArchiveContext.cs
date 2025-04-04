// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Auth;
using server.src.Persistence.Geography;
using server.src.Persistence.Metrics;
using server.src.Persistence.Support;
using server.src.Persistence.Weather;

namespace server.src.Persistence.Common.Contexts;

public class ArchiveContext : DbContext
{
    public AuthDbSets AuthDbSets { get; private set; }
    public GeographyDbSets GeographyDbSets { get; private set; }
    public MetricsDbSets MetricsDbSets { get; private set; }
    public SupportDbSets SupportDbSets { get; private set; }
    public WeatherDbSets WeatherDbSets { get; private set; }

    
    public ArchiveContext(DbContextOptions<ArchiveContext> options)
        : base(options)
    {
        AuthDbSets = new AuthDbSets(this);
        GeographyDbSets = new GeographyDbSets(this);
        MetricsDbSets = new MetricsDbSets(this);
        SupportDbSets = new SupportDbSets(this);
        WeatherDbSets = new WeatherDbSets(this);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.SetupAuth();
        modelBuilder.SetupGeography();
        modelBuilder.SetupMetrics();
        modelBuilder.SetupSupport();
        modelBuilder.SetupWeather();
    }
}
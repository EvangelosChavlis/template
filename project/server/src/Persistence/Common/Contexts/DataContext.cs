// packagess
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Auth;
using server.src.Persistence.Geography;
using server.src.Persistence.Metrics;
using server.src.Persistence.Weather;

namespace server.src.Persistence.Common.Contexts
{
    public class DataContext : DbContext
    {
        public AuthDbSets AuthDbSets { get; private set; }
        public GeographyDbSets GeographyDbSets { get; private set; }
        public MetricsDbSets MetricsDbSets { get; private set; }
        public WeatherDbSets WeatherDbSets { get; private set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            AuthDbSets = new AuthDbSets(this);
            GeographyDbSets = new GeographyDbSets(this);
            MetricsDbSets = new MetricsDbSets(this);
            WeatherDbSets = new WeatherDbSets(this);
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddAuth();
            modelBuilder.AddMetrics();
            modelBuilder.AddGeography();
            modelBuilder.AddWeather();
        }
    }
}
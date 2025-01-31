// Packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Configurations.Weather;

public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ForecastConfiguration(string tableName, string schema)
    {
       _tableName = tableName;
       _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Forecast> builder)
    {
       builder.HasKey(f => f.Id);

       builder.Property(f => f.Date)
              .IsRequired();

       builder.Property(f => f.TemperatureC)
              .IsRequired();

       builder.Property(f => f.FeelsLikeC)
              .IsRequired();

       builder.Property(f => f.Humidity)
              .IsRequired();

       builder.Property(f => f.WindSpeedKph)
              .IsRequired();

       builder.Property(f => f.WindDirection)
              .IsRequired();

       builder.Property(f => f.PressureHpa)
              .IsRequired();

       builder.Property(f => f.PrecipitationMm)
              .IsRequired();

       builder.Property(f => f.VisibilityKm)
              .IsRequired();

       builder.Property(f => f.UVIndex)
              .IsRequired();

       builder.Property(f => f.AirQualityIndex)
              .IsRequired();

       builder.Property(f => f.CloudCover)
              .IsRequired();

       builder.Property(f => f.LightningProbability)
              .IsRequired();

       builder.Property(f => f.PollenCount)
              .IsRequired();

       builder.Property(f => f.Sunrise)
              .IsRequired();

       builder.Property(f => f.Sunset)
              .IsRequired();

       builder.Property(f => f.Summary)
              .HasMaxLength(200);

       builder.Property(f => f.IsRead)
              .IsRequired();

       builder.Property(f => f.WarningId)
              .IsRequired();

       builder.Property(f => f.MoonPhaseId)
              .IsRequired();

       builder.HasOne(f => f.Warning)
              .WithMany(w => w.Forecasts)
              .HasForeignKey(f => f.WarningId)
              .OnDelete(DeleteBehavior.Cascade);

       builder.HasOne(f => f.MoonPhase)
               .WithMany(mp => mp.Forecasts)
               .HasForeignKey(f => f.MoonPhaseId)
               .OnDelete(DeleteBehavior.Restrict);

       builder.HasOne(f => f.Location)
               .WithMany(l => l.Forecasts)
               .HasForeignKey(f => f.LocationId)
               .OnDelete(DeleteBehavior.Restrict);

       builder.ToTable(_tableName, _schema);
    }
}
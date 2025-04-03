// Packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.Collections.Observations;

public class ObservationConfiguration : IEntityTypeConfiguration<Observation>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ObservationConfiguration(string tableName, string schema)
    {
       _tableName = tableName;
       _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Observation> builder)
    {
       builder.ConfigureBaseEntityProperties();

       builder.Property(o => o.Timestamp)
              .IsRequired();

       builder.Property(o => o.TemperatureC)
              .IsRequired();

       builder.Property(o => o.Humidity)
              .IsRequired();

       builder.Property(o => o.WindSpeedKph)
              .IsRequired();

       builder.Property(o => o.WindDirection)
              .IsRequired();

       builder.Property(o => o.PressureHpa)
              .IsRequired();

       builder.Property(o => o.PrecipitationMm)
              .IsRequired();

       builder.Property(o => o.VisibilityKm)
              .IsRequired();

       builder.Property(o => o.UVIndex)
              .IsRequired();

       builder.Property(o => o.AirQualityIndex)
              .IsRequired();

       builder.Property(o => o.CloudCover)
              .IsRequired();

       builder.Property(o => o.LightningProbability)
              .IsRequired();

       builder.Property(o => o.PollenCount)
              .IsRequired();

       builder.Property(o => o.MoonPhaseId)
              .IsRequired();

       builder.HasOne(o => o.MoonPhase)
               .WithMany(mp => mp.Observations)
               .HasForeignKey(o => o.MoonPhaseId)
               .OnDelete(DeleteBehavior.Restrict);

       builder.HasOne(o => o.Station)
               .WithMany(s => s.Observations)
               .HasForeignKey(o => o.StationId)
               .OnDelete(DeleteBehavior.Restrict);

       builder.ToTable(_tableName, _schema);
    }
}
// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Forecasts.Models;

namespace server.src.Persistence.Weather.Forecasts;

public class ForecastIndexes : IEntityTypeConfiguration<Forecast>
{
    public void Configure(EntityTypeBuilder<Forecast> builder)
    {
        builder.HasIndex(f => f.Id)
            .IsUnique()
            .HasDatabaseName($"{nameof(Forecast)}_{nameof(Forecast.Id)}");

        builder.HasIndex(f 
            => new { 
                f.Id, 
                f.Date, 
                f.TemperatureC,
                f.Humidity
            })
            .IsUnique()
             .HasDatabaseName($@"
                {nameof(Forecast)}_
                {nameof(Forecast.Id)}_
                {nameof(Forecast.Date)}_
                {nameof(Forecast.TemperatureC)}_
                {nameof(Forecast.Humidity)}
            ".Replace("\n", "").Trim());
    }
}

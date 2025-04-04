// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Collections.Forecasts.Models;

namespace server.src.Persistence.Weather.Collections.Forecasts;

public class ForecastIndexes : IEntityTypeConfiguration<Forecast>
{
    public void Configure(EntityTypeBuilder<Forecast> builder)
    {
        builder.HasIndex(f => f.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Forecast)}_
                {nameof(Forecast.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(f 
            => new { 
                f.Id, 
                f.Date, 
                f.TemperatureC,
                f.Humidity
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Forecast)}_
                {nameof(Forecast.Id)}_
                {nameof(Forecast.Date)}_
                {nameof(Forecast.TemperatureC)}_
                {nameof(Forecast.Humidity)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Weather;

namespace server.src.Persistence.Indexes.Weather;

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
                f.Humidity,
                f.IsRead,
            })
            .IsUnique()
             .HasDatabaseName($@"
                {nameof(Forecast)}_
                {nameof(Forecast.Id)}_
                {nameof(Forecast.Date)}_
                {nameof(Forecast.TemperatureC)}_
                {nameof(Forecast.Humidity)}_
                {nameof(Forecast.IsRead)}
            ".Replace("\n", "").Trim());
    }
}

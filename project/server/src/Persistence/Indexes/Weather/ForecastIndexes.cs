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
            .IsUnique();

        builder.HasIndex(f 
            => new { 
                f.Id, 
                f.Date, 
                f.TemperatureC, 
                f.IsRead
            })
            .IsUnique();
    }
}

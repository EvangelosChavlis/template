// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Indexes.Weather;

public class TelemetryIndexes : IEntityTypeConfiguration<Telemetry>
{
    public void Configure(EntityTypeBuilder<Telemetry> builder)
    {
        builder.HasIndex(t => t.Id)
            .IsUnique();

        builder.HasIndex(t 
            => new { 
                t.Id, 
                t.Method, 
                t.Path, 
                t.StatusCode,
                t.ResponseTime,
                t.RequestTimestamp
            })
            .IsUnique();
    }
}

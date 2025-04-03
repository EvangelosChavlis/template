// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Collections.Observations.Models;

namespace server.src.Persistence.Weather.Collections.Observations;

public class ObservationIndexes : IEntityTypeConfiguration<Observation>
{
    public void Configure(EntityTypeBuilder<Observation> builder)
    {
        builder.HasIndex(o => o.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Observation)}_
                {nameof(Observation.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(o 
            => new { 
                o.Id, 
                o.Timestamp, 
                o.TemperatureC,
                o.Humidity
            })
            .IsUnique()
             .HasDatabaseName($@"IX_
                {nameof(Observation)}_
                {nameof(Observation.Id)}_
                {nameof(Observation.Timestamp)}_
                {nameof(Observation.TemperatureC)}_
                {nameof(Observation.Humidity)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

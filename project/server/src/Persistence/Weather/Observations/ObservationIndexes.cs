// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Observations.Models;

namespace server.src.Persistence.Weather.Observations;

public class ObservationIndexes : IEntityTypeConfiguration<Observation>
{
    public void Configure(EntityTypeBuilder<Observation> builder)
    {
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
                {nameof(Observation.Humidity)}
            ".Replace("\n", "").Trim());
    }
}

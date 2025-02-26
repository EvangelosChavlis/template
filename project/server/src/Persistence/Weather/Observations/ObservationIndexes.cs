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
        builder.HasIndex(f => f.Id)
            .IsUnique()
            .HasDatabaseName($"{nameof(Observation)}_{nameof(Observation.Id)}");

        builder.HasIndex(f 
            => new { 
                f.Id, 
                f.Timestamp, 
                f.TemperatureC,
                f.Humidity
            })
            .IsUnique()
             .HasDatabaseName($@"
                {nameof(Observation)}_
                {nameof(Observation.Id)}_
                {nameof(Observation.Timestamp)}_
                {nameof(Observation.TemperatureC)}_
                {nameof(Observation.Humidity)}
            ".Replace("\n", "").Trim());
    }
}

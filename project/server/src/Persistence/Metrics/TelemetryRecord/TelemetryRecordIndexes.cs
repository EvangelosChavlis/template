// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



// source
using server.src.Domain.Metrics.TelemetryRecords.Models;

namespace server.src.Persistence.Metrics.TelemetryRecords;

public class TelemetryIndexes : IEntityTypeConfiguration<TelemetryRecord>
{
    public void Configure(EntityTypeBuilder<TelemetryRecord> builder)
    {
        builder.HasIndex(t => t.Id)
            .IsUnique()
            .HasDatabaseName($"{nameof(TelemetryRecord)}_{nameof(TelemetryRecord.Id)}");

        builder.HasIndex(t 
            => new { 
                t.Id, 
                t.Method, 
                t.Path, 
                t.StatusCode,
                t.ResponseTime,
                t.RequestTimestamp
            })
            .IsUnique()
            .HasDatabaseName($@"
                {nameof(TelemetryRecord)}_
                {nameof(TelemetryRecord.Id)}_
                {nameof(TelemetryRecord.Method)}_
                {nameof(TelemetryRecord.StatusCode)}_
                {nameof(TelemetryRecord.ResponseTime)}_
                {nameof(TelemetryRecord.RequestTimestamp)}
            ".Replace("\n", "").Trim());
    }
}

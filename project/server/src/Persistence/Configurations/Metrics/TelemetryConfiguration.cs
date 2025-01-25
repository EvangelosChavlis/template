// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Configurations.Metrics;

public class TelemetryConfiguration : IEntityTypeConfiguration<Telemetry>
{
    public void Configure(EntityTypeBuilder<Telemetry> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Method)
            .IsRequired();

        builder.Property(t => t.Path)
            .IsRequired();

        builder.Property(t => t.StatusCode)
            .IsRequired();

        builder.Property(t => t.ResponseTime)
            .IsRequired();

        builder.Property(t => t.MemoryUsed)
            .IsRequired();

        builder.Property(t => t.CPUusage)
            .IsRequired();

        builder.Property(t => t.RequestBodySize)
            .IsRequired();

        builder.Property(t => t.RequestTimestamp)
            .IsRequired();

        builder.Property(t => t.ResponseBodySize)
            .IsRequired();

        builder.Property(t => t.ResponseTimestamp)
            .IsRequired();

        builder.Property(t => t.ClientIp)
            .IsRequired();

        builder.Property(t => t.UserAgent)
            .IsRequired();

        builder.Property(t => t.ThreadId)
            .IsRequired();

        
        builder.HasOne(t => t.User)
            .WithMany(u => u.TelemetryRecords)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("TelemetryRecords");
    }
}
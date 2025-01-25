// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Configurations.Metrics;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.EntityId)
                .IsRequired();

        builder.Property(a => a.EntityType)
                .IsRequired()
                .HasConversion<string>();

        builder.Property(a => a.Action)
                .IsRequired()
                .HasConversion<string>();

        builder.Property(a => a.Timestamp)
                .IsRequired();

        builder.Property(a => a.UserId)
                .IsRequired();

        builder.Property(a => a.IPAddress)
                .HasMaxLength(45);

        builder.Property(a => a.Reason)
                .HasMaxLength(500);

        builder.Property(a => a.AdditionalMetadata)
                .HasColumnType("jsonb"); 

        builder.Property(a => a.BeforeValues)
                .HasColumnType("jsonb");

        builder.Property(a => a.AfterValues)
                .HasColumnType("jsonb");

        builder.HasOne(a => a.User)
                   .WithMany(u => u.AuditLogs)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Telemetry)
                   .WithMany(t => t.AuditLogs)
                   .HasForeignKey(a => a.TelemetryId)
                   .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("AuditLogs");
    }
}

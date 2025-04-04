// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Metrics.AuditLogs.Extensions;
using server.src.Domain.Metrics.AuditLogs.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Metrics.AuditLogs;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    private readonly string _tableName;
    private readonly string _schema;

    public AuditLogConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(a => a.EntityId)
                .IsRequired();

        builder.Property(a => a.EntityType)
                .IsRequired();

        builder.Property(a => a.Action)
                .IsRequired()
                .HasConversion<string>();

        builder.Property(a => a.Timestamp)
                .IsRequired();

        builder.Property(a => a.UserId)
                .IsRequired();

        builder.Property(a => a.IPAddress)
                .HasMaxLength(AuditLogSettings.IPAddressLength);

        builder.Property(a => a.Reason)
                .HasMaxLength(AuditLogSettings.ReasonLength);

        builder.Property(a => a.AdditionalMetadata)
                .HasColumnType(AuditLogSettings.AdditionalMetadataType); 

        builder.Property(a => a.BeforeValues)
                .HasColumnType(AuditLogSettings.BeforeValuesType);

        builder.Property(a => a.AfterValues)
                .HasColumnType(AuditLogSettings.AfterValuesType);

        builder.HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.TelemetryRecord)
                .WithMany(t => t.AuditLogs)
                .HasForeignKey(a => a.TelemetryId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}

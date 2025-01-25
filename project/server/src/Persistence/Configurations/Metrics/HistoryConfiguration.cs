// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Configurations.Metrics;

public class HistoryConfiguration : IEntityTypeConfiguration<History>
{
    public void Configure(EntityTypeBuilder<History> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.CreatedAt)
            .IsRequired();

        builder.HasOne(h => h.SourceTelemetry)
            .WithMany(t => t.Histories)
            .HasForeignKey(h => h.SourceTelemetryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.TargetTelemetry)
            .WithMany(t => t.Histories)
            .HasForeignKey(h => h.TargetTelemetryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Histories");
    }
}
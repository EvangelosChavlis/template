// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Configurations.Metrics;

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    private readonly string _tableName;
    private readonly string _schema;

    public StoryConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.CreatedAt)
            .IsRequired();

        builder.HasOne(h => h.SourceTelemetry)
            .WithMany(t => t.Stories)
            .HasForeignKey(h => h.SourceTelemetryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(h => h.TargetTelemetry)
            .WithMany(t => t.Stories)
            .HasForeignKey(h => h.TargetTelemetryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
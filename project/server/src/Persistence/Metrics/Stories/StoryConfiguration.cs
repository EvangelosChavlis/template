// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Metrics.Stories;

namespace server.src.Persistence.Metrics.Stories;

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

        builder.HasOne(h => h.SourceTelemetryRecord)
            .WithMany()
            .HasForeignKey(h => h.SourceTelemetryRecordId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(h => h.TargetTelemetryRecord)
            .WithMany()
            .HasForeignKey(h => h.TargetTelemetryRecordId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(_tableName, _schema);
    }
}
// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Metrics.Trails;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Metrics.Trails;

public class TrailConfiguration : IEntityTypeConfiguration<Trail>
{
    private readonly string _tableName;
    private readonly string _schema;

    public TrailConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Trail> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(c => c.Timestamp)
                .IsRequired();

        builder.HasOne(c => c.SourceAuditLog)
               .WithMany()
               .HasForeignKey(c => c.SourceAuditLogId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.TargetAuditLog)
               .WithMany()
               .HasForeignKey(c => c.TargetAuditLogId)
               .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable(_tableName, _schema);
    }
}

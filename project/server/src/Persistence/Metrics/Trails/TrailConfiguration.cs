// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Metrics.Trails;

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
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Timestamp)
                .IsRequired();

        builder.HasOne(c => c.SourceAuditLog)
                .WithMany(a => a.Trails)
                .HasForeignKey(c => c.SourceAuditLogId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.TargetAuditLog)
                .WithMany(a => a.Trails) 
                .HasForeignKey(c => c.TargetAuditLogId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}

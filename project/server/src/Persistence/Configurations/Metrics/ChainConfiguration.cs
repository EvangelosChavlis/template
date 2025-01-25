// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Configurations.Metrics;

public class ChainConfiguration : IEntityTypeConfiguration<Chain>
{
    public void Configure(EntityTypeBuilder<Chain> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Timestamp)
                .IsRequired();

        builder.HasOne(c => c.SourceAuditLog)
                .WithMany(a => a.Chains)
                .HasForeignKey(c => c.SourceAuditLogId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.TargetAuditLog)
                .WithMany(a => a.Chains) 
                .HasForeignKey(c => c.TargetAuditLogId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Chains");
    }
}

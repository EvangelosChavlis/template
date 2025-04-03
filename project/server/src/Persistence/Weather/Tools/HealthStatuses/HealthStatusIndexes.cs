// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.HealthStatuses.Models;

namespace server.src.Persistence.Weather.Tools.HealthStatuses;

public class HealthStatusIndexes : IEntityTypeConfiguration<HealthStatus>
{
    public void Configure(EntityTypeBuilder<HealthStatus> builder)
    {
        builder.HasIndex(hs => hs.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(HealthStatus)}_
                {nameof(HealthStatus.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(hs => hs.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(HealthStatus)}_
                {nameof(HealthStatus.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(hs 
            => new { 
                hs.Id, 
                hs.Name
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(HealthStatus)}_
                {nameof(HealthStatus.Id)}_
                {nameof(HealthStatus.Name)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(hs 
            => new { 
                hs.Id, 
                hs.Name, 
                hs.Code,
                hs.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(HealthStatus)}_
                {nameof(HealthStatus.Id)}_
                {nameof(HealthStatus.Name)}_
                {nameof(HealthStatus.Code)}_
                {nameof(HealthStatus.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.HealthStatuses.Extensions;
using server.src.Domain.Weather.Tools.HealthStatuses.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.Tools.HealthStatuses;

public class HealthStatusConfiguration : IEntityTypeConfiguration<HealthStatus>
{
    private readonly string _tableName;
    private readonly string _schema;

    public HealthStatusConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<HealthStatus> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(hs => hs.Name)
            .IsRequired()
            .HasMaxLength(HealthStatusSettings.NameLength);

        builder.Property(hs => hs.Description)
            .IsRequired()
            .HasMaxLength(HealthStatusSettings.DescriptionLength);

        builder.Property(hs => hs.Code)
            .IsRequired()
            .HasMaxLength(HealthStatusSettings.CodeLength);

        builder.Property(hs => hs.IsActive)
            .IsRequired();

        builder.HasMany(hs => hs.Sensors)
            .WithOne(s => s.Status)
            .HasForeignKey(s => s.HealthStatusId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}
// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.ClimateZones.Extensions;
using server.src.Domain.Geography.Natural.ClimateZones.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Natural.ClimateZones;

public class ClimateZoneConfiguration : IEntityTypeConfiguration<ClimateZone>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ClimateZoneConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<ClimateZone> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(cz => cz.Name)
            .IsRequired()
            .HasMaxLength(ClimateZoneSettings.NameLength);

        builder.Property(cz => cz.Description)
            .IsRequired()
            .HasMaxLength(ClimateZoneSettings.DescriptionLength);

        builder.Property(cz => cz.Code)
            .IsRequired()
            .HasMaxLength(ClimateZoneSettings.CodeLength);

        builder.Property(cz => cz.AvgTemperatureC)
            .IsRequired();

        builder.Property(cz => cz.AvgPrecipitationMm)
            .IsRequired();

        builder.Property(cz => cz.IsActive)
            .IsRequired();

        builder.HasMany(cz => cz.Locations)
            .WithOne(l => l.ClimateZone)
            .HasForeignKey(l => l.ClimateZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
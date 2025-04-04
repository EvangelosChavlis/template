// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.Sensors.Extensions;
using server.src.Domain.Weather.Tools.Sensors.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.Tools.Sensors;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    private readonly string _tableName;
    private readonly string _schema;

    public SensorConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.ConfigureBaseEntityProperties();
        
        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(SensorSettings.NameLength);

        builder.Property(s => s.Manufacturer)
            .IsRequired()
            .HasMaxLength(SensorSettings.ManufacturerLength);

        builder.Property(s => s.SN)
            .IsRequired()
            .HasMaxLength(SensorSettings.SNLength);

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.HasOne(s => s.Unit)
            .WithMany(u => u.Sensors)
            .HasForeignKey(s => s.UnitId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Status)
            .WithMany(h => h.Sensors)
            .HasForeignKey(s => s.HealthStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Station)
            .WithMany(st => st.Sensors)
            .HasForeignKey(s => s.StationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Series)
            .WithOne(se => se.Sensor)
            .HasForeignKey(se => se.SensorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}
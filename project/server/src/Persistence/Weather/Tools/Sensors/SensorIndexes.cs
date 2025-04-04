// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.Sensors.Models;

namespace server.src.Persistence.Weather.Tools.Sensors;

public class SensorIndexes : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.HasIndex(s => s.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Sensor)}_
                {nameof(Sensor.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id, 
                s.Name, 
                s.SN,
                s.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Sensor)}_
                {nameof(Sensor.Id)}_
                {nameof(Sensor.Name)}_
                {nameof(Sensor.SN)}_
                {nameof(Sensor.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

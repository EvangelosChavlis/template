// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using TimeZone = server.src.Domain.Models.Geography.TimeZone;

namespace server.src.Persistence.Configurations.Geography;

public class TimeZoneConfiguration : IEntityTypeConfiguration<TimeZone>
{
    private readonly string _tableName;
    private readonly string _schema;

    public TimeZoneConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<TimeZone> builder)
    {

        builder.HasKey(tz => tz.Id);

        builder.Property(tz => tz.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(tz => tz.UtcOffset)
            .IsRequired();

        builder.Property(tz => tz.SupportsDaylightSaving)
            .IsRequired();

        builder.Property(tz => tz.IsActive)
            .IsRequired();

        builder.HasMany(tz => tz.Locations)
            .WithOne(l => l.TimeZone)
            .HasForeignKey(l => l.TimeZoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Timezones.Models;

namespace server.src.Persistence.Configurations.Geography.Timezones;

public class TimezoneConfiguration : IEntityTypeConfiguration<Timezone>
{
    private readonly string _tableName;
    private readonly string _schema;

    public TimezoneConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Timezone> builder)
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
            .WithOne(l => l.Timezone)
            .HasForeignKey(l => l.TimezoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
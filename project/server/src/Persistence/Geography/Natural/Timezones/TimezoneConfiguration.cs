// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Persistence.Geography.Natural.Timezones;

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

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(t => t.UtcOffset)
            .IsRequired();

        builder.Property(t => t.SupportsDaylightSaving)
            .IsRequired();

        builder.Property(t => t.IsActive)
            .IsRequired();

        builder.HasMany(t => t.Locations)
            .WithOne(l => l.Timezone)
            .HasForeignKey(l => l.TimezoneId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
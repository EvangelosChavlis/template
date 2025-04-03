// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.src.Domain.Geography.Natural.Timezones.Extensions;

// source
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Configuration;

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
        builder.ConfigureBaseEntityProperties();

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(TimezoneLength.NameLength);
            
        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(TimezoneLength.DescriptionLength);

        builder.Property(t => t.Code)
            .IsRequired()
            .HasMaxLength(TimezoneLength.CodeLength);

        builder.Property(t => t.UtcOffset)
            .IsRequired();

        builder.Property(t => t.DstOffset)
            .IsRequired(false);

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
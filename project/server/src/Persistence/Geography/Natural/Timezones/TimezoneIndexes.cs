// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.Timezones.Models;

namespace server.src.Persistence.Geography.Natural.Timezones;

public class TimezoneIndexes : IEntityTypeConfiguration<Timezone>
{
    public void Configure(EntityTypeBuilder<Timezone> builder)
    {
        builder.HasIndex(t 
            => new { 
                t.Id, 
                t.Name,
                t.UtcOffset,
                t.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Timezone)}_
                {nameof(Timezone.Id)}_
                {nameof(Timezone.Name)}_
                {nameof(Timezone.UtcOffset)}_
                {nameof(Timezone.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

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
        builder.HasIndex(c => c.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Timezone)}_
                {nameof(Timezone.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(c => c.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Timezone)}_
                {nameof(Timezone.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(t 
            => new { 
                t.Id, 
                t.Name,
                t.Code,
                t.UtcOffset,
                t.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Timezone)}_
                {nameof(Timezone.Id)}_
                {nameof(Timezone.Name)}_
                {nameof(Timezone.Code)}_
                {nameof(Timezone.UtcOffset)}_
                {nameof(Timezone.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

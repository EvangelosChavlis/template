// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Collections.MoonPhases.Models;

namespace server.src.Persistence.Weather.Collections.MoonPhases;

public class MoonPhaseIndexes : IEntityTypeConfiguration<MoonPhase>
{
    public void Configure(EntityTypeBuilder<MoonPhase> builder)
    {
        builder.HasIndex(m => m.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(MoonPhase)}_
                {nameof(MoonPhase.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(m 
            => new { 
                m.Id, 
                m.Name, 
                m.Code 
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(MoonPhase)}_
                {nameof(MoonPhase.Id)}_
                {nameof(MoonPhase.Name)}_
                {nameof(MoonPhase.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
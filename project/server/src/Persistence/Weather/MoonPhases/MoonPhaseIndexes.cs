// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.MoonPhases.Models;

namespace server.src.Persistence.Weather.MoonPhases;

public class MoonPhaseIndexes : IEntityTypeConfiguration<MoonPhase>
{
    public void Configure(EntityTypeBuilder<MoonPhase> builder)
    {
        builder.HasIndex(m => m.Id)
            .IsUnique();

        builder.HasIndex(m 
            => new { 
                m.Id, 
                m.Name, 
                m.Description 
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(MoonPhase)}_
                {nameof(MoonPhase.Id)}_
                {nameof(MoonPhase.Name)}_
                {nameof(MoonPhase.Description)}
            ".Replace("\n", "").Trim());
    }
}
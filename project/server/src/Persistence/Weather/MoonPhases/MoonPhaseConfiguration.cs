// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.MoonPhases;

public class MoonPhaseConfiguration : IEntityTypeConfiguration<MoonPhase>
{
    private readonly string _tableName;
    private readonly string _schema;

    public MoonPhaseConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<MoonPhase> builder)
    {
        builder.ConfigureBaseEntityProperties();
        
        builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100); 

        builder.Property(m => m.Description)
                .HasMaxLength(500);

        builder.Property(m => m.IlluminationPercentage)
            .IsRequired();

        builder.Property(m => m.PhaseOrder)
            .IsRequired();

        builder.Property(m => m.DurationDays)
            .IsRequired();

        builder.Property(m => m.IsSignificant)
            .IsRequired();

        builder.Property(m => m.OccurrenceDate)
            .IsRequired();

        builder.HasMany(m => m.Forecasts)
                .WithOne(f => f.MoonPhase)
                .HasForeignKey(f => f.MoonPhaseId)
                .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable(_tableName, _schema);
    }
}

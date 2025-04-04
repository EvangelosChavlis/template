// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.States.Extensions;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.States;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    private readonly string _tableName;
    private readonly string _schema;

    public StateConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(StateSettings.NameLength);

        builder.Property(s => s.Description)
            .IsRequired(false)
            .HasMaxLength(StateSettings.DescriptionLength);

        builder.Property(s => s.Capital)
            .IsRequired(false)
            .HasMaxLength(StateSettings.CapitalLength);

        builder.Property(s => s.Population)
            .IsRequired();

        builder.Property(s => s.AreaKm2)
            .IsRequired();

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(StateSettings.CodeLength);

        builder.Property(s => s.IsActive)
            .IsRequired();

        builder.HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.Regions)
            .WithOne(r => r.State)
            .HasForeignKey(r => r.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
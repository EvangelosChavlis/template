// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Continents.Extensions;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Continents;

public class ContinentConfiguration : IEntityTypeConfiguration<Continent>
{
    private readonly string _tableName;
    private readonly string _schema;

    public ContinentConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Continent> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(ContinentSettings.NameLength);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(ContinentSettings.CodeLength);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(ContinentSettings.DescriptionLength);

        builder.Property(c => c.AreaKm2)
            .IsRequired();

        builder.Property(c => c.Population)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasMany(c => c.Countries)
            .WithOne(c => c.Continent)
            .HasForeignKey(c => c.ContinentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
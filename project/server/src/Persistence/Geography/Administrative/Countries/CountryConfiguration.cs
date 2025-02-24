// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Geography.Administrative.Countries;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    private readonly string _tableName;
    private readonly string _schema;

    public CountryConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.Property(c => c.IsoCode)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(c => c.Capital)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(c => c.Population)
            .IsRequired();

        builder.Property(c => c.AreaKm2)
            .IsRequired();

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasOne(c => c.Continent)
            .WithMany(c => c.Countries)
            .HasForeignKey(c => c.ContinentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.States)
            .WithOne(s => s.Country)
            .HasForeignKey(s => s.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
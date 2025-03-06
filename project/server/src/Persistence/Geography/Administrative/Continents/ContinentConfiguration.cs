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
            .HasMaxLength(ContinentLength.NameLength);

        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(ContinentLength.CodeLength);

        builder.Property(c => c.Description)
            .IsRequired(false)
            .HasMaxLength(ContinentLength.DescriptionLength);

        builder.Property(c => c.IsActive)
            .IsRequired();

        builder.HasMany(c => c.Countries)
            .WithOne(c => c.Continent)
            .HasForeignKey(c => c.ContinentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable(_tableName, _schema);
    }
}
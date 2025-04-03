// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.Units.Extensions;
using server.src.Domain.Weather.Tools.Units.Models;
using server.src.Persistence.Common.Configuration;

namespace server.src.Persistence.Weather.Tools.Units;

public class UnitConfiguration : IEntityTypeConfiguration<Unit>
{
    private readonly string _tableName;
    private readonly string _schema;

    public UnitConfiguration(string tableName, string schema)
    {
        _tableName = tableName;
        _schema = schema;
    }

    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.ConfigureBaseEntityProperties();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(UnitLength.NameLength);

        builder.Property(u => u.Symbol)
            .IsRequired()
            .HasMaxLength(UnitLength.SymbolLength);

        builder.Property(u => u.Description)
            .IsRequired()
            .HasMaxLength(UnitLength.DescriptionLength);

        builder.Property(u => u.IsActive)
            .IsRequired();

        builder.HasMany(u => u.Sensors)
            .WithOne(s => s.Unit)
            .HasForeignKey(s => s.UnitId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.ToTable(_tableName, _schema);
    }
}
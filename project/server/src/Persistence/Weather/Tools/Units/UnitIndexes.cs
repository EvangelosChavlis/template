// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Weather.Tools.Units.Models;

namespace server.src.Persistence.Weather.Tools.Units;

public class UnitIndexes : IEntityTypeConfiguration<Unit>
{
    public void Configure(EntityTypeBuilder<Unit> builder)
    {
        builder.HasIndex(u => u.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Unit)}_
                {nameof(Unit.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(u => u.Symbol)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Unit)}_
                {nameof(Unit.Symbol)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(u 
            => new { 
                u.Id, 
                u.Name
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Unit)}_
                {nameof(Unit.Id)}_
                {nameof(Unit.Name)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(u 
            => new { 
                u.Id, 
                u.Name, 
                u.Symbol,
                u.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Unit)}_
                {nameof(Unit.Id)}_
                {nameof(Unit.Name)}_
                {nameof(Unit.Symbol)}_
                {nameof(Unit.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

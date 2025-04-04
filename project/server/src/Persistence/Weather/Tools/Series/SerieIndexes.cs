// packages
using Microsoft.EntityFrameworkCore;

// source
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.src.Domain.Weather.Tools.Series.Models;

namespace server.src.Persistence.Weather.Tools.Series;

public class SerieIndexes : IEntityTypeConfiguration<Serie>
{
    public void Configure(EntityTypeBuilder<Serie> builder)
    {
        builder.HasIndex(s => s.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Serie)}_
                {nameof(Serie.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id, 
                s.Value, 
                s.Timestamp
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Serie)}_
                {nameof(Serie.Id)}_
                {nameof(Serie.Value)}_
                {nameof(Serie.Timestamp)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}
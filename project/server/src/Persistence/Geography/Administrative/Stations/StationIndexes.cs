// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Persistence.Geography.Administrative.Stations;

public class StationIndexes : IEntityTypeConfiguration<Station>
{
    public void Configure(EntityTypeBuilder<Station> builder)
    {
        builder.HasIndex(s => s.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Station)}_
                {nameof(Station.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s => s.Code)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Station)}_
                {nameof(Station.Code)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id,
                s.Code,
                s.LocationId
            })
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(Station)}_
                {nameof(Station.Id)}_
                {nameof(Station.Code)}_
                {nameof(Station.LocationId)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id, 
                s.Name,
                s.Code,
                s.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(Station)}_
                {nameof(Station.Id)}_
                {nameof(Station.Name)}_
                {nameof(Station.Code)}_
                {nameof(Station.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

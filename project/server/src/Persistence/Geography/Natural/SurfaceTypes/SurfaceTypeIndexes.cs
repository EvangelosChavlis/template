// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// source
using server.src.Domain.Geography.Natural.SurfaceTypes.Models;

namespace server.src.Persistence.Geography.Natural.SurfaceTypes;

public class SurfaceTypeIndexes : IEntityTypeConfiguration<SurfaceType>
{
    public void Configure(EntityTypeBuilder<SurfaceType> builder)
    {
        builder.HasIndex(s => s.Id)
            .IsUnique()
            .HasDatabaseName(@$"IX_
                {nameof(SurfaceType)}_
                {nameof(SurfaceType.Id)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());

        builder.HasIndex(s 
            => new { 
                s.Id, 
                s.Name,
                s.Description,
                s.IsActive
            })
            .IsUnique()
            .HasDatabaseName($@"IX_
                {nameof(SurfaceType)}_
                {nameof(SurfaceType.Id)}_
                {nameof(SurfaceType.Name)}_
                {nameof(SurfaceType.Description)}_
                {nameof(SurfaceType.IsActive)}"
            .Replace("\r\n", "")
            .Replace(" ", "")
            .Trim());
    }
}

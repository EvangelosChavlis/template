using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Geography.ClimateZones.Models;
using server.src.Domain.Geography.Locations.Models;
using server.src.Domain.Geography.TerrainTypes.Models;
using server.src.Domain.Geography.Timezones.Models;

namespace server.src.Persistence.Geography;

public class GeographyContext : DbContext
{
    public DbSet<ClimateZone> ClimateZones { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<TerrainType> TerrainTypes { get; set; }
    public DbSet<Timezone> Timezones { get; set; }

    public GeographyContext(DbContextOptions<GeographyContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddGeography();
    }
}

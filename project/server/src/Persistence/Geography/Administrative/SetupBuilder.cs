// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Geography.Administrative.Continents;
using server.src.Persistence.Geography.Administrative.Countries;
using server.src.Persistence.Geography.Administrative.Districts;
using server.src.Persistence.Geography.Administrative.Municipalities;
using server.src.Persistence.Geography.Administrative.Neighborhoods;
using server.src.Persistence.Geography.Administrative.Regions;
using server.src.Persistence.Geography.Administrative.States;

namespace server.src.Persistence.Geography.Administrative;

public static class SetupBuilder
{
    private readonly static string _administrativeSchema = "geography_administrative";

    public static void AddAdministrative(this ModelBuilder modelBuilder)
    {
        #region Configuration
        modelBuilder.ApplyConfiguration(new ContinentConfiguration("Continents", _administrativeSchema));
        modelBuilder.ApplyConfiguration(new CountryConfiguration("Countries", _administrativeSchema));
        modelBuilder.ApplyConfiguration(new DistrictConfiguration("Districts", _administrativeSchema));
        modelBuilder.ApplyConfiguration(new MunicipalityConfiguration("Municipalities", _administrativeSchema));
        modelBuilder.ApplyConfiguration(new NeighborhoodConfiguration("Neighborhoods", _administrativeSchema));
        modelBuilder.ApplyConfiguration(new RegionConfiguration("Regions", _administrativeSchema));
        modelBuilder.ApplyConfiguration(new StateConfiguration("States", _administrativeSchema));
        #endregion
    }
}
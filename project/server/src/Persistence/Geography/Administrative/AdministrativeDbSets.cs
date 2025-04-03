// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Domain.Geography.Administrative.Stations.Models;

namespace server.src.Persistence.Geography.Administrative;

public class AdministrativeDbSets
{
    public DbSet<Continent> Continents { get; private set; }
    public DbSet<Country> Countries { get; private set; }
    public DbSet<State> States { get; private set; }
    public DbSet<Station> Stations { get; private set; }
    public DbSet<Region> Regions { get; private set; }
    public DbSet<Municipality> Municipalities { get; private set; }
    public DbSet<District> Districts { get; private set; }
    public DbSet<Neighborhood> Neighborhoods { get; private set; }

    public AdministrativeDbSets(DbContext context)
    {
        Continents = context.Set<Continent>();
        Countries = context.Set<Country>();
        States = context.Set<State>();
        Stations = context.Set<Station>();
        Regions = context.Set<Region>();
        Municipalities = context.Set<Municipality>();
        Districts = context.Set<District>();
        Neighborhoods = context.Set<Neighborhood>();
    }
}
// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Domain.Weather.Collections.Observations.Models;
using server.src.Domain.Weather.Collections.Warnings.Models;

namespace server.src.Persistence.Weather.Collections;

public class CollectionsDbSets
{
    public DbSet<Forecast> Forecasts { get; private set; }
    public DbSet<MoonPhase> MoonPhases { get; private set; }
    public DbSet<Observation> Observations { get; private set; }
    public DbSet<Warning> Warnings { get; private set; }

    public CollectionsDbSets(DbContext context)
    {
        Forecasts = context.Set<Forecast>();
        MoonPhases = context.Set<MoonPhase>();
        Observations = context.Set<Observation>();
        Warnings = context.Set<Warning>();
    }
}
// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Weather.Tools.HealthStatuses.Models;
using server.src.Domain.Weather.Tools.Units.Models;

namespace server.src.Persistence.Weather.Collections;

public class ToolsDbSets
{
    public DbSet<HealthStatus> HealthStatuses { get; private set; }
    public DbSet<Unit> Units { get; private set; }

    public ToolsDbSets(DbContext context)
    {
        HealthStatuses = context.Set<HealthStatus>();
        Units = context.Set<Unit>();
    }
}
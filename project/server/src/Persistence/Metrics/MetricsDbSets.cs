using Microsoft.EntityFrameworkCore;
using server.src.Domain.Metrics.AuditLogs.Models;
using server.src.Domain.Metrics.LogErrors.Models;
using server.src.Domain.Metrics.Stories;
using server.src.Domain.Metrics.TelemetryRecords.Models;
using server.src.Domain.Metrics.Trails;

namespace server.src.Persistence.Metrics;

public class MetricsDbSets
{
    public DbSet<AuditLog> AuditLogs { get; private set; }
    public DbSet<Trail> Trails { get; private set; }
    public DbSet<LogError> LogErrors { get; private set; }
    public DbSet<TelemetryRecord> TelemetryRecords { get; private set; }
    public DbSet<Story> Stories { get; private set; }

    public MetricsDbSets(DbContext context)
    {
        AuditLogs = context.Set<AuditLog>();
        Trails = context.Set<Trail>();
        LogErrors = context.Set<LogError>();
        TelemetryRecords = context.Set<TelemetryRecord>();
        Stories = context.Set<Story>();
    }
}
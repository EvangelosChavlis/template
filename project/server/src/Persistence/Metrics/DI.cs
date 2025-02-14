// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Persistence.Metrics.AuditLogs;
using server.src.Persistence.Metrics.LogErrors;
using server.src.Persistence.Metrics.Stories;
using server.src.Persistence.Metrics.TelemetryRecords;
using server.src.Persistence.Metrics.Trails;

namespace server.src.Persistence.Metrics;

public static class DI
{
    private readonly static string _metricsSchema = "metrics";

    public static void AddMetrics(this ModelBuilder modelBuilder)
    {
        #region Metrics Configuration
        modelBuilder.ApplyConfiguration(new AuditLogConfiguration("AuditLogs", _metricsSchema));
        modelBuilder.ApplyConfiguration(new TrailConfiguration("Trails", _metricsSchema));
        modelBuilder.ApplyConfiguration(new LogErrorConfiguration("LogErrors", _metricsSchema));
        modelBuilder.ApplyConfiguration(new TelemetryRecordConfiguration("TelemetryRecords", _metricsSchema));
        modelBuilder.ApplyConfiguration(new StoryConfiguration("Stories", _metricsSchema));
        #endregion
    }
}
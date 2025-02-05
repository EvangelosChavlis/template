// packages
using Microsoft.EntityFrameworkCore;

// source
using server.src.Domain.Metrics.AuditLogs.Models;
using server.src.Domain.Metrics.LogErrors.Models;
using server.src.Domain.Metrics.Stories;
using server.src.Domain.Metrics.TelemetryRecords.Models;
using server.src.Domain.Metrics.Trails;

namespace server.src.Persistence.Metrics;

public class MetricsContext : DbContext
{
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<Trail> Trails { get; set; }
    public DbSet<LogError> LogErrors { get; set; }
    public DbSet<TelemetryRecord> TelemetryRecords { get; set; }
    public DbSet<Story> Stories { get; set; }

    public MetricsContext(DbContextOptions<MetricsContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AddMetrics();
    }
}
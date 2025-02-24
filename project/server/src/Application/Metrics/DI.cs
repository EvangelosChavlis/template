// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Metrics.LogErrors;
using server.src.Application.Metrics.Telemetry;

namespace server.src.Application.Metrics;

public static class DI
{
    public static IServiceCollection RegisterMetrics(this IServiceCollection services)
    {
        services.RegisterLogErrors();
        services.RegisterTelemetry();
        
        return services;
    }
}
// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Telemetry.Interfaces;
using server.src.Application.Common.Interfaces;
using server.src.Application.Metrics.Telemetry.Queries;
using server.src.Application.Telemetry.Services;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;

namespace server.src.Application.Metrics.Telemetry;

public static class DependencyInjection
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services)
    {        
        // register query handlers
        services.AddScoped<IRequestHandler<GetTelemetryQuery, ListResponse<List<ListItemTelemetryDto>>>, GetTelemetryHandler>();
        services.AddScoped<IRequestHandler<GetTelemetryByUserIdQuery, ListResponse<List<ListItemTelemetryDto>>>, GetTelemetryByUserIdHandler>();
        services.AddScoped<IRequestHandler<GetTelemetryByIdQuery, Response<ItemTelemetryDto>>, GetTelemetryByIdHandler>();

        // register queries
        services.AddScoped<ITelemetryQueries, TelemetryQueries>();

        return services;
    }
}

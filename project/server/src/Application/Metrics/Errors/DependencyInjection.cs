// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Metrics.Errors.Interfaces;
using server.src.Application.Metrics.Errors.Queries;
using server.src.Application.Metrics.Errors.Services;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;

namespace server.src.Application.Metrics.Errors;

public static class DependencyInjection
{
    public static IServiceCollection AddError(this IServiceCollection services)
    {        
        // register query handlers
        services.AddScoped<IRequestHandler<GetErrorsQuery, ListResponse<List<ListItemErrorDto>>>, GetErrorsHandler>();
        services.AddScoped<IRequestHandler<GetErrorByIdQuery, Response<ItemErrorDto>>, GetErrorByIdHandler>();

        // register queries
        services.AddScoped<IErrorQueries, ErrorQueries>();

        return services;
    }
}

// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Interfaces;
using server.src.Application.Weather.Forecasts.Commands;
using server.src.Application.Weather.Forecasts.Interfaces;
using server.src.Application.Weather.Forecasts.Queries;
using server.src.Application.Weather.Forecasts.Services;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Weather.Forecasts;

public static class DependencyInjection
{
    public static IServiceCollection AddForecasts(this IServiceCollection services)
    {
        services.AddMemoryCache();
                
        // register query handlers
        services.AddScoped<IRequestHandler<GetForecastsStatsQuery, Response<List<StatItemForecastDto>>>, GetForecastsStatsHandler>();
        services.AddScoped<IRequestHandler<GetForecastsQuery, ListResponse<List<ListItemForecastDto>>>, GetForecastsHandler>();
        services.AddScoped<IRequestHandler<GetForecastByIdQuery, Response<ItemForecastDto>>, GetForecastByIdHandler>();

        // register queries
        services.AddScoped<IForecastQueries, ForecastQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<InitializeForecastsCommand, Response<string>>, InitializeForecastsHandler>();
        services.AddScoped<IRequestHandler<CreateForecastCommand, Response<string>>, CreateForecastHandler>();
        services.AddScoped<IRequestHandler<UpdateForecastCommand, Response<string>>, UpdateForecastHandler>();
        services.AddScoped<IRequestHandler<DeleteForecastCommand, Response<string>>, DeleteForecastHandler>();

        // register commands
        services.AddScoped<IForecastCommands, ForecastCommands>();

        return services;
    }
}

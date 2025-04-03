// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Weather.Collections.Forecasts;
using server.src.Application.Weather.Collections.MoonPhases;
using server.src.Application.Weather.Collections.Observations;
using server.src.Application.Weather.Collections.Warnings;

namespace server.src.Application.Weather.Collections;

public static class DI
{
    public static IServiceCollection RegisterCollections(this IServiceCollection services)
    {
        services.RegisterForecasts();
        services.RegisterMoonPhases();
        services.RegisterObservations();
        services.RegisterWarnings();

        return services;
    }
}
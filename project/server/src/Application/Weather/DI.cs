// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Weather.Forecasts;
using server.src.Application.Weather.MoonPhases;
using server.src.Application.Weather.Observations;
using server.src.Application.Weather.Warnings;

namespace server.src.Application.Weather;

public static class DI
{
    public static IServiceCollection RegisterWeather(this IServiceCollection services)
    {
        services.RegisterForecasts();
        services.RegisterMoonPhases();
        services.RegisterObservations();
        services.RegisterWarnings();

        return services;
    }
}
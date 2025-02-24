// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth;
using server.src.Application.Common;
using server.src.Application.Data;
using server.src.Application.Geography;
using server.src.Application.Metrics;
using server.src.Application.Weather;

namespace server.src.Application;

public static class DI
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.RegisterAuth();
        services.RegisterCommon();
        services.RegisterData();
        services.RegisterGeography();
        services.RegisterMetrics();
        services.RegisterWeather();

        return services;
    }
}
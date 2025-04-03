// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Weather.Collections;

namespace server.src.Application.Weather;

public static class DI
{
    public static IServiceCollection RegisterWeather(this IServiceCollection services)
    {
        services.RegisterCollections();

        return services;
    }
}
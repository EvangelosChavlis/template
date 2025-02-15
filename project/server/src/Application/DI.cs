// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles;
using server.src.Application.Auth.UserLogins;
using server.src.Application.Auth.UserLogouts;
using server.src.Application.Auth.UserRoles;
using server.src.Application.Auth.Users;
using server.src.Application.Common;
using server.src.Application.Data;
using server.src.Application.Metrics.Errors;
using server.src.Application.Metrics.Telemetry;
using server.src.Application.Weather.Forecasts;
using server.src.Application.Weather.MoonPhases;
using server.src.Application.Weather.Observations;
using server.src.Application.Weather.Warnings;

namespace server.src.Application;

public static class DI
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Common
        services.AddCommon();

        // Data
        services.AddData();

        // Auth
        services.AddRoles();
        services.AddUsers();
        services.AddUserRoles();
        services.AddUserLogins();
        services.AddUserLogouts();

        // Weather
        services.AddForecasts();
        services.AddMoonPhases();
        services.AddObservations();
        services.AddWarnings();
        
        //Metrics
        services.AddTelemetry();
        services.AddLogErrors();

        return services;
    }
}
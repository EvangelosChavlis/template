// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles;
using server.src.Application.Auth.UserLogins;
using server.src.Application.Auth.UserLogouts;
using server.src.Application.Auth.UserRoles;
using server.src.Application.Auth.Users;
using server.src.Application.Data;
using server.src.Application.Helpers;
using server.src.Application.Metrics.Errors;
using server.src.Application.Metrics.Telemetry;
using server.src.Application.Weather.Forecasts;
using server.src.Application.Weather.Warnings;

namespace server.src.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Data
        services.AddData();

        // Auth
        services.AddUsers();
        services.AddRoles();
        services.AddUserRoles();
        services.AddUserLogins();
        services.AddUserLogouts();

        // Weather
        services.AddWarnings();
        services.AddForecasts();
        
        //Metrics
        services.AddTelemetry();
        services.AddError();

        // Helpers
        services.AddScoped<IAuthHelper, AuthHelper>();

        return services;
    }
}
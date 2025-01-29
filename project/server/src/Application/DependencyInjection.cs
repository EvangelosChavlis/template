// packages
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles;
using server.src.Application.Auth.UserRoles;
using server.src.Application.Auth.Users;
using server.src.Application.Helpers;
using server.src.Application.Interfaces.Auth.UserLogins;
using server.src.Application.Interfaces.Auth.UserLogouts;
using server.src.Application.Interfaces.Data;
using server.src.Application.Metrics.Errors;
using server.src.Application.Metrics.Telemetry;
using server.src.Application.Services.Auth.UserLogins;
using server.src.Application.Services.Auth.UserLogouts;
using server.src.Application.Services.Data;
using server.src.Application.Validators.Auth;
using server.src.Application.Weather.Forecasts;
using server.src.Application.Weather.Warnings;
using server.src.Domain.Dto.Auth;

namespace server.src.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Data
        services.AddScoped<IDataCommands, DataCommands>();

        //Auth
        services.AddScoped<IUserLoginQueries, UserLoginQueries>();
        services.AddScoped<IUserLoginCommands, UserLoginCommands>();
        services.AddScoped<IUserLogoutQueries, UserLogoutQueries>();
        services.AddScoped<IUserLogoutCommands, UserLogoutCommands>();

        // Auth
        services.AddUsers();
        services.AddRoles();
        services.AddUserRoles();

        // Weather
        services.AddWarnings();
        services.AddForecasts();
        
        //Metrics
        services.AddTelemetry();
        services.AddError();

        // Helpers
        services.AddScoped<IAuthHelper, AuthHelper>();

        //Auth/Users
        services.AddScoped<IValidator<UserLoginDto>, UserLoginValidators>();

        return services;
    }
}
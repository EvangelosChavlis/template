// packages
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using server.src.Application.Auth.Roles;
using server.src.Application.Auth.Users;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces.Auth.UserLogins;
using server.src.Application.Interfaces.Auth.UserLogouts;
using server.src.Application.Interfaces.Auth.UserRoles;
using server.src.Application.Interfaces.Data;
using server.src.Application.Interfaces.Metrics;
using server.src.Application.Services.Auth.UserLogins;
using server.src.Application.Services.Auth.UserLogouts;
using server.src.Application.Services.Auth.UserRoles;
using server.src.Application.Services.Data;
using server.src.Application.Services.Metrics;
using server.src.Application.Validators.Auth;
using server.src.Application.Validators.Forecast;
using server.src.Application.Validators.Role;
using server.src.Application.Validators.Warning;
using server.src.Application.Weather.Forecasts;
using server.src.Application.Weather.Warnings;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Weather;

namespace server.src.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        //Data
        services.AddScoped<IDataCommands, DataCommands>();

        //Auth
        services.AddScoped<IUserRoleCommands, UserRoleCommands>();
        services.AddScoped<IUserLoginQueries, UserLoginQueries>();
        services.AddScoped<IUserLoginCommands, UserLoginCommands>();
        services.AddScoped<IUserLogoutQueries, UserLogoutQueries>();
        services.AddScoped<IUserLogoutCommands, UserLogoutCommands>();

        // Auth
        services.AddUsers();
        services.AddRoles();

        // Weather
        services.AddWarnings();
        services.AddForecasts();
        
        //Metrics
        services.AddScoped<IErrorQueries, ErrorQueries>();
        services.AddScoped<ITelemetryQueries, TelemetryQueries>();

        // Helpers
        services.AddScoped<IAuthHelper, AuthHelper>();
        // services.AddScoped<IAuditLogHelper, AuditLogHelper>();

        services.AddFluentValidationAutoValidation();

        //Auth/Roles
        services.AddScoped<IValidator<RoleDto>, RoleValidators>();
        services.AddScoped<IValidator<RoleDto>, RoleValidators>();

        //Auth/Users
        services.AddScoped<IValidator<UserDto>, UserValidators>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginValidators>();
        services.AddScoped<IValidator<ForgotPasswordDto>, ForgotPasswordValidators>();
        services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordValidators>();
        services.AddScoped<IValidator<Verify2FADto>, Verify2FAValidator>();

        //Weather
        services.AddScoped<IValidator<ForecastDto>, ForecastValidators>();
        services.AddScoped<IValidator<WarningDto>, WarningValidators>();
        
        return services;
    }
}
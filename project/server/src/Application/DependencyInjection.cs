// packages
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Interfaces.Auth;
using server.src.Application.Interfaces.Data;
using server.src.Application.Interfaces.Metrics;
using server.src.Application.Interfaces.Weather;
using server.src.Application.Services.Auth;
using server.src.Application.Services.Data;
using server.src.Application.Services.Metrics;
using server.src.Application.Services.Weather;
using server.src.Application.Validators.Auth;
using server.src.Application.Validators.Forecast;
using server.src.Application.Validators.Role;
using server.src.Application.Validators.Warning;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Weather;

namespace server.src.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        //Data
        services.AddScoped<IDataService, DataService>();

        //Auth
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        //Weather
        services.AddScoped<IWarningsService, WarningsService>();
        services.AddScoped<IForecastsService, ForecastsService>();

        //Metrics
        services.AddScoped<IErrorsService, ErrorsService>();
        services.AddScoped<ITelemetryService, TelemetryService>();

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
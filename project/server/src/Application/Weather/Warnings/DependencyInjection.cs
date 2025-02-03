// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Weather.Warnings.Commands;
using server.src.Application.Weather.Warnings.Interfaces;
using server.src.Application.Weather.Warnings.Queries;
using server.src.Application.Weather.Warnings.Services;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;

namespace server.src.Application.Weather.Warnings;

public static class DependencyInjection
{
    public static IServiceCollection AddWarnings(this IServiceCollection services)
    {       
        // register query handlers 
        services.AddScoped<IRequestHandler<GetWarningsQuery, ListResponse<List<ListItemWarningDto>>>, GetWarningsHandler>();
        services.AddScoped<IRequestHandler<GetWarningsPickerQuery, Response<List<PickerWarningDto>>>, GetWarningsPickerHandler>();
        services.AddScoped<IRequestHandler<GetWarningByIdQuery, Response<ItemWarningDto>>, GetWarningByIdHandler>();

        // register queries
        services.AddScoped<IWarningQueries, WarningQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<InitializeWarningsCommand, Response<string>>, InitializeWarningsHandler>();
        services.AddScoped<IRequestHandler<CreateWarningCommand, Response<string>>, CreateWarningHandler>();
        services.AddScoped<IRequestHandler<UpdateWarningCommand, Response<string>>, UpdateWarningHandler>();
        services.AddScoped<IRequestHandler<DeleteWarningCommand, Response<string>>, DeleteWarningHandler>();

        // register commands
        services.AddScoped<IWarningCommands, WarningCommands>();

        return services;
    }
}

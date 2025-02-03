// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles.Services;
using server.src.Application.Data.Commands;
using server.src.Application.Data.Interfaces;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddData(this IServiceCollection services)
    {   
        // register command handlers
        services.AddScoped<IRequestHandler<SeedDataCommand, Response<string>>, SeedDataHandler>();
        services.AddScoped<IRequestHandler<ClearDataCommand, Response<string>>, ClearDataHandler>();

        // register commands
        services.AddScoped<IDataCommands, DataCommands>();

        return services;
    }
}

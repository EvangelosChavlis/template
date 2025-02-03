// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.UserLogins.Services;
using server.src.Application.Auth.UserLogouts.Commands;
using server.src.Application.Auth.UserLogouts.Interfaces;
using server.src.Application.Auth.UserLogouts.Queries;
using server.src.Application.Auth.UserLogouts.Services;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserLogouts;

public static class DependencyInjection
{
    public static IServiceCollection AddUserLogouts(this IServiceCollection services)
    {        
        // register query handlers
        services.AddScoped<IRequestHandler<GetLogoutsByUserIdQuery, ListResponse<List<ListItemUserLogoutDto>>>, GetLogoutsByUserIdHandler>();
        services.AddScoped<IRequestHandler<GetUserLogoutByIdQuery, Response<ItemUserLogoutDto>>, GetUserLogoutByIdHandler>();

        // register queries
        services.AddScoped<IUserLogoutQueries, UserLogoutQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<UserLogoutCommand, Response<string>>, UserLogoutHandler>();

        // register commands
        services.AddScoped<IUserLogoutCommands, UserLogoutCommands>();

        return services;
    }
}

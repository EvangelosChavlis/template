// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.UserLogins.Commands;
using server.src.Application.Auth.UserLogins.Interfaces;
using server.src.Application.Auth.UserLogins.Queries;
using server.src.Application.Auth.UserLogins.Services;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserLogins;

public static class DependencyInjection
{
    public static IServiceCollection AddUserLogins(this IServiceCollection services)
    {        
        // register query handlers
        services.AddScoped<IRequestHandler<GetLoginsByUserIdQuery, ListResponse<List<ListItemUserLoginDto>>>, GetLoginsByUserIdHandler>();
        services.AddScoped<IRequestHandler<GetUserLoginByIdQuery, Response<ItemUserLoginDto>>, GetUserLoginByIdHandler>();

        // register queries
        services.AddScoped<IUserLoginQueries, UserLoginQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<UserLoginCommand, Response<AuthenticatedUserDto>>, UserLoginHandler>();

        // register commands
        services.AddScoped<IUserLoginCommands, UserLoginCommands>();

        return services;
    }
}

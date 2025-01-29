using Microsoft.Extensions.DependencyInjection;
using server.src.Application.Auth.UserRoles.Commands;
using server.src.Application.Auth.UserRoles.Interfaces;
using server.src.Application.Auth.UserRoles.Services;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserRoles;

public static class DependencyInjection
{
    public static IServiceCollection AddUserRoles(this IServiceCollection services)
    {
        // // register query handlers        
        // services.AddScoped<IRequestHandler<GetUsersQuery, ListResponse<List<ListItemUserDto>>>, GetUsersHandler>();
        // services.AddScoped<IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>>, GetUserByIdHandler>();

        // // register queries
        // services.AddScoped<IUserRoleQueries, UserRoleQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<AssingRoleToUserCommand, Response<string>>, AssingRoleToUserHandler>();
        services.AddScoped<IRequestHandler<UnassignRoleFromUserCommand, Response<string>>, UnassignRoleFromUserHandler>();
    
        // register commands
        services.AddScoped<IUserRoleCommands, UserRoleCommands>();

        return services;
    }
}
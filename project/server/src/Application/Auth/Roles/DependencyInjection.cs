// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Auth.Roles.Commands;
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Auth.Roles.Queries;
using server.src.Application.Auth.Roles.Services;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Roles;

public static class DependencyInjection
{
    public static IServiceCollection AddRoles(this IServiceCollection services)
    {        
        // register query handlers
        services.AddScoped<IRequestHandler<GetRolesQuery, ListResponse<List<ListItemRoleDto>>>, GetRolesHandler>();
        services.AddScoped<IRequestHandler<GetRolesByUserIdQuery, ListResponse<List<ItemRoleDto>>>, GetRolesByUserIdHandler>();
        services.AddScoped<IRequestHandler<GetRolesPickerQuery, Response<List<PickerRoleDto>>>, GetRolesPickerHandler>();
        services.AddScoped<IRequestHandler<GetRoleByIdQuery, Response<ItemRoleDto>>, GetRoleByIdHandler>();

        // register queries
        services.AddScoped<IRoleQueries, RoleQueries>();

        // register command handlers
        services.AddScoped<IRequestHandler<InitializeRolesCommand, Response<string>>, InitializeRolesHandler>();
        services.AddScoped<IRequestHandler<CreateRoleCommand, Response<string>>, CreateRoleHandler>();
        services.AddScoped<IRequestHandler<UpdateRoleCommand, Response<string>>, UpdateRoleHandler>();
        services.AddScoped<IRequestHandler<DeleteRoleCommand, Response<string>>, DeleteRoleHandler>();

        // register commands
        services.AddScoped<IRoleCommands, RoleCommands>();

        return services;
    }
}

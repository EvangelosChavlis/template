// source
using server.src.Application.Auth.UserRoles.Commands;
using server.src.Application.Auth.UserRoles.Interfaces;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.UserRoles.Services;

public class UserRoleCommands : IUserRoleCommands
{
    private readonly IRequestHandler<AssingRoleToUserCommand, Response<string>> _assingRoleToUserHander;
    private readonly IRequestHandler<UnassignRoleFromUserCommand, Response<string>> _unassignRoleFromUserHandler;

    public UserRoleCommands(
        IRequestHandler<AssingRoleToUserCommand, Response<string>> assingRoleToUserHander,
        IRequestHandler<UnassignRoleFromUserCommand, Response<string>> unassignRoleFromUserHandler)
    {
        _assingRoleToUserHander = assingRoleToUserHander;
        _unassignRoleFromUserHandler = unassignRoleFromUserHandler;
    }


    public async Task<Response<string>> AssignRoleToUserAsync(Guid userId, 
        Guid roleId, CancellationToken token = default)
    {
        var command = new AssingRoleToUserCommand(userId, roleId);
        return await _assingRoleToUserHander.Handle(command, token);
    }

    public async Task<Response<string>> UnassignRoleFromUserAsync(Guid userId, 
        Guid roleId, CancellationToken token = default)
    {
        var command = new UnassignRoleFromUserCommand(userId, roleId);
        return await _unassignRoleFromUserHandler.Handle(command, token);
    }

}
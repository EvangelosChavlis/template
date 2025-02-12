// source
using server.src.Application.Auth.UserRoles.Commands;
using server.src.Application.Auth.UserRoles.Interfaces;
using server.src.Application.Common.Services;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Auth.UserRoles.Services;

public class UserRoleCommands : IUserRoleCommands
{
    private readonly RequestExecutor _requestExecutor;

    public UserRoleCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }


    public async Task<Response<string>> AssignRoleToUserAsync(Guid userId, 
        Guid roleId, CancellationToken token = default)
    {
        var command = new AssingRoleToUserCommand(userId, roleId);
        return await _requestExecutor.Execute<AssingRoleToUserCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UnassignRoleFromUserAsync(Guid userId, 
        Guid roleId, CancellationToken token = default)
    {
        var command = new UnassignRoleFromUserCommand(userId, roleId);
        return await _requestExecutor.Execute<UnassignRoleFromUserCommand, Response<string>>(command, token);
    }

}
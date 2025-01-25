// source
using server.src.Application.Auth.Roles.Commands;
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;

namespace server.src.Application.Auth.Roles.Services;

public class RoleCommands : IRoleCommands
{
    private readonly IRequestHandler<InitializeRolesCommand, Response<string>> _initializeRolesHander;
    private readonly IRequestHandler<CreateRoleCommand, Response<string>> _createRoleHandler;
    private readonly IRequestHandler<UpdateRoleCommand, Response<string>> _updateRoleHandler;
    private readonly IRequestHandler<ActivateRoleCommand, Response<string>> _activateRoleHandler;
    private readonly IRequestHandler<DeactivateRoleCommand, Response<string>> _deactivateRoleHandler;
    private readonly IRequestHandler<DeleteRoleCommand, Response<string>> _deleteRoleHandler;

    public RoleCommands(
        IRequestHandler<InitializeRolesCommand, Response<string>> initializeRolesHander,
        IRequestHandler<CreateRoleCommand, Response<string>> createRoleHandler,
        IRequestHandler<UpdateRoleCommand, Response<string>> updateRoleHandler,
        IRequestHandler<ActivateRoleCommand, Response<string>> activateRoleHandler,
        IRequestHandler<DeactivateRoleCommand, Response<string>> deactivateRoleHandler,
        IRequestHandler<DeleteRoleCommand, Response<string>> deleteRoleHandler)
    {
        _initializeRolesHander = initializeRolesHander;
        _createRoleHandler = createRoleHandler;
        _updateRoleHandler = updateRoleHandler;
        _activateRoleHandler = activateRoleHandler;
        _deactivateRoleHandler = deactivateRoleHandler;
        _deleteRoleHandler = deleteRoleHandler;
    }

    public async Task<Response<string>> InitializeRolesAsync(List<RoleDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeRolesCommand(dto);
        return await _initializeRolesHander.Handle(command, token);
    }

    public async Task<Response<string>> CreateRoleAsync(RoleDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateRoleCommand(dto);
        return await _createRoleHandler.Handle(command, token);
    }

    public async Task<Response<string>> UpdateRoleAsync(Guid id, RoleDto dto, 
        CancellationToken token = default)
    {
        var command = new UpdateRoleCommand(id, dto);
        return await _updateRoleHandler.Handle(command, token);
    }

    public async Task<Response<string>> ActivateRoleAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new ActivateRoleCommand(id, version);
        return await _activateRoleHandler.Handle(command, token);
    }

    public async Task<Response<string>> DeactivateRoleAsync(Guid id,
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateRoleCommand(id, version);
        return await _deactivateRoleHandler.Handle(command, token);
    }

    public async Task<Response<string>> DeleteRoleAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteRoleCommand(id, version);
        return await _deleteRoleHandler.Handle(command, token);
    }
}
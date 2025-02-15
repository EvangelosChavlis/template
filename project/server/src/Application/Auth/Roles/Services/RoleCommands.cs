// source
using server.src.Application.Auth.Roles.Commands;
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Common.Services;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Common.Dtos;

namespace server.src.Application.Auth.Roles.Services;

public class RoleCommands : IRoleCommands
{
    private readonly RequestExecutor _requestExecutor;

    public RoleCommands(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<Response<string>> InitializeRolesAsync(List<CreateRoleDto> dto, 
        CancellationToken token = default)
    {
        var command = new InitializeRolesCommand(dto);
        return await _requestExecutor
            .Execute<InitializeRolesCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> CreateRoleAsync(CreateRoleDto dto, 
        CancellationToken token = default)
    {
        var command = new CreateRoleCommand(dto);
        return await _requestExecutor
            .Execute<CreateRoleCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> UpdateRoleAsync(Guid id, UpdateRoleDto dto, CancellationToken token = default)
    {
        var command = new UpdateRoleCommand(id, dto);
        return await _requestExecutor
            .Execute<UpdateRoleCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> ActivateRoleAsync(Guid id, Guid version, CancellationToken token = default)
    {
        var command = new ActivateRoleCommand(id, version);
        return await _requestExecutor
            .Execute<ActivateRoleCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeactivateRoleAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeactivateRoleCommand(id, version);
        return await _requestExecutor
            .Execute<DeactivateRoleCommand, Response<string>>(command, token);
    }

    public async Task<Response<string>> DeleteRoleAsync(Guid id, 
        Guid version, CancellationToken token = default)
    {
        var command = new DeleteRoleCommand(id, version);
        return await _requestExecutor
            .Execute<DeleteRoleCommand, Response<string>>(command, token);
    }
}

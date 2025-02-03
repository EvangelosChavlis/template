// source
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Application.Auth.Roles.Queries;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.Roles.Services;

public class RoleQueries : IRoleQueries
{
    private readonly IRequestHandler<GetRolesQuery, ListResponse<List<ItemRoleDto>>> _getRolesHandler;
    private readonly IRequestHandler<GetRolesByUserIdQuery, ListResponse<List<ItemRoleDto>>> _getRolesByUserIdHandler;
    private readonly IRequestHandler<GetRolesPickerQuery, Response<List<PickerRoleDto>>> _getRolesPickerHandler;
    private readonly IRequestHandler<GetRoleByIdQuery, Response<ItemRoleDto>> _getRoleByIdHandler;

    public RoleQueries(
        IRequestHandler<GetRolesQuery, ListResponse<List<ItemRoleDto>>> getRolesHandler,
        IRequestHandler<GetRolesByUserIdQuery, ListResponse<List<ItemRoleDto>>> getRolesByUserIdHandler,
        IRequestHandler<GetRolesPickerQuery, Response<List<PickerRoleDto>>> getRolePickerHandler,
        IRequestHandler<GetRoleByIdQuery, Response<ItemRoleDto>> getRoleByIdHandler)
    {
        _getRolesHandler = getRolesHandler;
        _getRolesByUserIdHandler = getRolesByUserIdHandler;
        _getRolesPickerHandler = getRolePickerHandler;
        _getRoleByIdHandler = getRoleByIdHandler;
    }

    public async Task<ListResponse<List<ItemRoleDto>>> GetRolesAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetRolesQuery(urlQuery);
        return await _getRolesHandler.Handle(query, token);
    }

    public async Task<ListResponse<List<ItemRoleDto>>> GetRolesByUserIdAsync(Guid id, UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetRolesByUserIdQuery(id, urlQuery);
        return await _getRolesByUserIdHandler.Handle(query, token);
    }

    public async Task<Response<List<PickerRoleDto>>> GetRolesPickerAsync(CancellationToken token = default)
    {
        var query = new GetRolesPickerQuery();
        return await _getRolesPickerHandler.Handle(query, token);
    }

    public async Task<Response<ItemRoleDto>> GetRoleByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetRoleByIdQuery(id);
        return await _getRoleByIdHandler.Handle(query, token);
    }
}
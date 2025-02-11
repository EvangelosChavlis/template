// source
using server.src.Application.Common.Services;
using server.src.Application.Auth.Roles.Interfaces;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;
using server.src.Application.Auth.Roles.Queries;

namespace server.src.Application.Auth.Roles.Services;

public class RoleQueries : IRoleQueries
{
    private readonly RequestExecutor _requestExecutor;

    public RoleQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemRoleDto>>> GetRolesAsync(UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetRolesQuery(urlQuery);
        return await _requestExecutor.Execute<GetRolesQuery, ListResponse<List<ListItemRoleDto>>>(query, token);
    }

    public async Task<ListResponse<List<ItemRoleDto>>> GetRolesByUserIdAsync(Guid id, UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetRolesByUserIdQuery(id, urlQuery);
        return await _requestExecutor.Execute<GetRolesByUserIdQuery, ListResponse<List<ItemRoleDto>>>(query, token);
    }

    public async Task<Response<List<PickerRoleDto>>> GetRolesPickerAsync(CancellationToken token = default)
    {
        var query = new GetRolesPickerQuery();
        return await _requestExecutor.Execute<GetRolesPickerQuery, Response<List<PickerRoleDto>>>(query, token);
    }

    public async Task<Response<ItemRoleDto>> GetRoleByIdAsync(Guid id, CancellationToken token = default)
    {
        var query = new GetRoleByIdQuery(id);
        return await _requestExecutor.Execute<GetRoleByIdQuery, Response<ItemRoleDto>>(query, token);
    }
}
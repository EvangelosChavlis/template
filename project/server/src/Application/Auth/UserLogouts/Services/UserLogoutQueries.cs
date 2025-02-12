// source
using server.src.Application.Auth.UserLogouts.Interfaces;
using server.src.Application.Auth.UserLogouts.Queries;
using server.src.Application.Common.Services;
using server.src.Domain.Auth.UserLogouts;
using server.src.Domain.Auth.UserLogouts.Dtos;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.UserLogouts.Services;

public class UserLogoutQueries : IUserLogoutQueries
{
    private readonly RequestExecutor _requestExecutor;

    public UserLogoutQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemUserLogoutDto>>> GetLogoutsByUserIdAsync(Guid id, 
        UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetLogoutsByUserIdQuery(id, urlQuery);
        return await _requestExecutor.Execute<GetLogoutsByUserIdQuery, ListResponse<List<ListItemUserLogoutDto>>>(query, token);
    }

    public async Task<Response<ItemUserLogoutDto>> GetUserLogoutByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetUserLogoutByIdQuery(id);
        return await _requestExecutor.Execute<GetUserLogoutByIdQuery, Response<ItemUserLogoutDto>>(query, token);
    }
}
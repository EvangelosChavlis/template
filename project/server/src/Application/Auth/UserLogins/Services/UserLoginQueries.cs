// source
using server.src.Application.Auth.UserLogins.Interfaces;
using server.src.Application.Auth.UserLogins.Queries;
using server.src.Application.Common.Services;
using server.src.Domain.Auth.UserLogins.Dtos;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.UserLogins.Services;

public class UserLoginQueries : IUserLoginQueries
{
    private readonly RequestExecutor _requestExecutor;

    public UserLoginQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemUserLoginDto>>> GetLoginsByUserIdAsync(Guid id, 
        UrlQuery urlQuery, CancellationToken token = default)
    {
        var query = new GetLoginsByUserIdQuery(id, urlQuery);
        return await _requestExecutor.Execute<GetLoginsByUserIdQuery, ListResponse<List<ListItemUserLoginDto>>>(query, token);
    }

    public async Task<Response<ItemUserLoginDto>> GetUserLoginByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetUserLoginByIdQuery(id);
        return await _requestExecutor.Execute<GetUserLoginByIdQuery, Response<ItemUserLoginDto>>(query, token);
    }
}
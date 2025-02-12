// source
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Auth.Users.Queries;
using server.src.Application.Common.Services;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Models;

namespace server.src.Application.Auth.Users.Services;

public class UserQueries : IUserQueries
{
    private readonly RequestExecutor _requestExecutor;

    public UserQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemUserDto>>> GetUsersAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetUsersQuery(urlQuery);
        return await _requestExecutor.Execute<GetUsersQuery, ListResponse<List<ListItemUserDto>>>(query, token);
    }


    public async Task<Response<ItemUserDto>> GetUserByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetUserByIdQuery(id);
        return await _requestExecutor.Execute<GetUserByIdQuery, Response<ItemUserDto>>(query, token);
    }
}
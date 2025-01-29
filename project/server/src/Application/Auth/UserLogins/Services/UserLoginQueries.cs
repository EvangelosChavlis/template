// source
using server.src.Application.Auth.UserLogins.Interfaces;
using server.src.Application.Auth.UserLogins.Queries;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.UserLogins.Services;

public class UserLoginQueries : IUserLoginQueries
{
    private readonly IRequestHandler<GetLoginsByUserIdQuery, ListResponse<List<ListItemUserLoginDto>>> _getLoginsByUserIdHandler;
    private readonly IRequestHandler<GetUserLoginByIdQuery, Response<ItemUserLoginDto>> _getUserLoginByIdHandler;

    public UserLoginQueries(
        IRequestHandler<GetLoginsByUserIdQuery, ListResponse<List<ListItemUserLoginDto>>> getLoginsByUserIdHandler,
        IRequestHandler<GetUserLoginByIdQuery, Response<ItemUserLoginDto>> getUserLoginByIdHandler)
    {
        _getLoginsByUserIdHandler = getLoginsByUserIdHandler;
        _getUserLoginByIdHandler = getUserLoginByIdHandler;
    }

    public async Task<ListResponse<List<ListItemUserLoginDto>>> GetLoginsByUserIdAsync(Guid id, UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetLoginsByUserIdQuery(id, urlQuery);
        return await _getLoginsByUserIdHandler.Handle(query, token);
    }

    public async Task<Response<ItemUserLoginDto>> GetUserLoginByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetUserLoginByIdQuery(id);
        return await _getUserLoginByIdHandler.Handle(query, token);
    }
}
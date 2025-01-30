// source
using server.src.Application.Auth.UserLogouts.Interfaces;
using server.src.Application.Auth.UserLogouts.Queries;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.UserLogouts.Services;

public class UserLogoutQueries : IUserLogoutQueries
{
    private readonly IRequestHandler<GetLogoutsByUserIdQuery, ListResponse<List<ListItemUserLogoutDto>>> _getLogoutsByUserIdHandler;
    private readonly IRequestHandler<GetUserLogoutByIdQuery, Response<ItemUserLogoutDto>> _getUserLogoutByIdHandler;

    public UserLogoutQueries(
        IRequestHandler<GetLogoutsByUserIdQuery, ListResponse<List<ListItemUserLogoutDto>>> getLogoutsByUserIdHandler,
        IRequestHandler<GetUserLogoutByIdQuery, Response<ItemUserLogoutDto>> getUserLogoutByIdHandler)
    {
        _getLogoutsByUserIdHandler = getLogoutsByUserIdHandler;
        _getUserLogoutByIdHandler = getUserLogoutByIdHandler;
    }

    public async Task<ListResponse<List<ListItemUserLogoutDto>>> GetLogoutsByUserIdAsync(Guid id, UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetLogoutsByUserIdQuery(id, urlQuery);
        return await _getLogoutsByUserIdHandler.Handle(query, token);
    }

    public async Task<Response<ItemUserLogoutDto>> GetUserLogoutByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetUserLogoutByIdQuery(id);
        return await _getUserLogoutByIdHandler.Handle(query, token);
    }
}
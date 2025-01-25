// source
using server.src.Application.Auth.Users.Interfaces;
using server.src.Application.Auth.Users.Queries;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Common;

namespace server.src.Application.Auth.Users.Services;

public class UserQueries : IUserQueries
{
    private readonly IRequestHandler<GetUsersQuery, ListResponse<List<ListItemUserDto>>> _getUsersHandler;
    private readonly IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>> _getUserByIdHandler;

    public UserQueries(
        IRequestHandler<GetUsersQuery, ListResponse<List<ListItemUserDto>>> getUsersHandler,
        IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>> getUserByIdHandler)
    {
        _getUsersHandler = getUsersHandler;
        _getUserByIdHandler = getUserByIdHandler;
    }

    public async Task<ListResponse<List<ListItemUserDto>>> GetUsersAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetUsersQuery(urlQuery);
        return await _getUsersHandler.Handle(query, token);
    }


    public async Task<Response<ItemUserDto>> GetUserByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetUserByIdQuery(id);
        return await _getUserByIdHandler.Handle(query, token);
    }
}
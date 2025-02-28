// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.UserLogins.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.UserLogins.Dtos;
using server.src.Domain.Auth.UserLogins.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.UserLogins.Queries;

public record GetUserLoginByIdQuery(Guid Id) : IRequest<Response<ItemUserLoginDto>>;

public class GetUserLoginByIdHandler : IRequestHandler<GetUserLoginByIdQuery, Response<ItemUserLoginDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetUserLoginByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemUserLoginDto>> Handle(GetUserLoginByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<UserLogin, object>>[] { ul => ul.User };
        var filters = new Expression<Func<UserLogin, bool>>[] { ul => ul.Id == query.Id};
        var userLogin = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);

        // Check for existence
        if (userLogin is null)
            return new Response<ItemUserLoginDto>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserLoginDtoMapper.ErrorItemUserLoginDtoMapping());

        // Mapping
        var dto = userLogin.ItemUserLoginDtoMapping();

        // Initializing object
        return new Response<ItemUserLoginDto>()
            .WithMessage("User fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
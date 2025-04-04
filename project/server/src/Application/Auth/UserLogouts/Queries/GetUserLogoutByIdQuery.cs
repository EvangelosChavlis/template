// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.UserLogouts.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.UserLogouts.Dtos;
using server.src.Domain.Auth.UserLogouts.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.UserLogouts.Queries;

public record GetUserLogoutByIdQuery(Guid Id) : IRequest<Response<ItemUserLogoutDto>>;

public class GetUserLogoutByIdHandler : IRequestHandler<GetUserLogoutByIdQuery, Response<ItemUserLogoutDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetUserLogoutByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemUserLogoutDto>> Handle(GetUserLogoutByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<UserLogout, object>>[] { ul => ul.User };
        var filters = new Expression<Func<UserLogout, bool>>[] { ul => ul.Id == query.Id};
        var userLogin = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);

        // Check for existence
        if (userLogin is null)
            return new Response<ItemUserLogoutDto>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserLogoutDtoMapper.ErrorItemUserLogoutDtoMapping());

        // Mapping
        var dto = userLogin.ItemUserLogoutDtoMapping();

        // Initializing object
        return new Response<ItemUserLogoutDto>()
            .WithMessage("User logout item fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
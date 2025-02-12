// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Users.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<Response<ItemUserDto>>;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetUserByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemUserDto>> Handle(GetUserByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var filters = new Expression<Func<User, bool>>[] { u => u.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (user is null)
            return new Response<ItemUserDto>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Mapping
        var dto = user.ItemUserDtoMapping();

        // Initializing object
        return new Response<ItemUserDto>()
            .WithMessage("User fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
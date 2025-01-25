// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Users.Mappings;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<Response<ItemUserDto>>;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public GetUserByIdHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemUserDto>> Handle(GetUserByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<User, object>>[] { };
        var filters = new Expression<Func<User, bool>>[] { x => x.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, filters, includes, token);

        if (user is null)
            return new Response<ItemUserDto>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(UserMappings.ErrorItemUserDtoMapping());


        var dto = user.ItemUserDtoMapping();

        return new Response<ItemUserDto>()
            .WithMessage("User fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;
     
public record UnlockUserCommand(Guid Id) : IRequest<Response<string>>;

public class UnlockUserHandler : IRequestHandler<UnlockUserCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public UnlockUserHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(UnlockUserCommand command, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id};
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in unlocking user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {command.Id} not found.");

        if (!user.LockoutEnabled)
            return new Response<string>()
                .WithMessage("Error in unlocking user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("User is unlocked.");

        user.LockoutEnabled = false;
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            return new Response<string>()
                .WithMessage("Error in unlocking user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to lock unlock.");
    

        return new Response<string>()
            .WithMessage("Success in unlocking user.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"User {user.UserName} unlocked successfully");
    }
}
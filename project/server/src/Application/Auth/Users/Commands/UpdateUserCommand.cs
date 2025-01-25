// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record UpdateUserCommand(Guid Id, UserDto Dto) : IRequest<Response<string>>;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public UpdateUserHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(UpdateUserCommand command, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User with not found.");

        command.Dto.UpdateUserModelMapping(user);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            return new Response<string>()
                .WithMessage("Error updating user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to update user.");
    
        // Initializing object
        return new Response<string>()
            .WithMessage("Success updating user.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"User {user.UserName} updated successfully!");
    }
}
// packages
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record RefreshTokenCommand(string AuthToken) : IRequest<Response<string>>;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    private readonly IAuthHelper _authHelper;

    public RefreshTokenHandler(DataContext context, ICommonRepository commonRepository, 
        IAuthHelper authHelper)
    {
        _context = context;
        _commonRepository = commonRepository;
        _authHelper = authHelper;
    }

    public async Task<Response<string>> Handle(RefreshTokenCommand command, CancellationToken token = default)
    {
        // Implementation for refreshing JWT token
        var principal = _authHelper.GetPrincipalFromExpiredToken(command.AuthToken);
        if (principal is null)
            return new Response<string>()
                .WithMessage("Error in refreshing token.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Invalid token.");

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Ensure userId is a valid Guid before proceeding
        if (userId is null || !Guid.TryParse(userId, out var userGuid))
        {
            return new Response<string>()
                .WithMessage("Invalid user ID.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("The user ID is invalid.");
        }

        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == userGuid }; // Compare with Guid
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in refreshing token.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");

        var newToken = await _authHelper.GenerateJwtToken(user, token);

        return new Response<string>()
            .WithMessage("Success in refreshing token.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(newToken);
    }
}
// packages
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record RefreshTokenCommand(string AuthToken) : IRequest<Response<string>>;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IAuthHelper _authHelper;

    public RefreshTokenHandler(ICommonRepository commonRepository, IAuthHelper authHelper)
    {
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

        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == userGuid }; // Compare with Guid
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
            return new Response<string>()
                .WithMessage("Error in refreshing token.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");

        // Generating Token
        var newToken = await _authHelper.GenerateJwtToken(user, token);

        // Generating failed
        if (!newToken.Success)
        {
            return new Response<string>()
                .WithMessage("An error occurred while generating token. Please try again.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(newToken.Success)
                .WithData(newToken.Data!);
        }

        // Check for existence
        if (!newToken.Success)
            return new Response<string>()
                .WithMessage("Error in refreshing token.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");


        // Initializing object
        return new Response<string>()
            .WithMessage("Success in refreshing token.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(newToken.Data!);
    }
}
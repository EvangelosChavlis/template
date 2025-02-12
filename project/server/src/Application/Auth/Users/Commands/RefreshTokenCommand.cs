// packages
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;

// source
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record RefreshTokenCommand(string AuthToken) : IRequest<Response<string>>;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly ICommonCommands _commonCommands;

    public RefreshTokenHandler(ICommonRepository commonRepository, ICommonQueries commonQueries,
        ICommonCommands commonCommands)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _commonCommands = commonCommands;
    }

    public async Task<Response<string>> Handle(RefreshTokenCommand command, CancellationToken token = default)
    {
        // Implementation for refreshing JWT token
        var principal = await _commonQueries.GetPrincipalFromExpiredToken(command.AuthToken, token);
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
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == userGuid };
        var user = await _commonRepository.GetResultByIdAsync(userFilters, token: token);

        // Check for existence
        if (user is null)
            return new Response<string>()
                .WithMessage("Error in refreshing token.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");

        // Generating Token
        var newToken = await _commonCommands.GenerateJwtToken(user.Id, user.UserName, 
            user.Email, user.SecurityStamp, token);

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
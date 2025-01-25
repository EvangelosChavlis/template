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

public record Verify2FACommand(Guid Id, string AuthToken) : IRequest<Response<string>>;

public class Verify2FAHandler : IRequestHandler<Verify2FACommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public Verify2FAHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(Verify2FACommand command, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in verifying 2FA token")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");

        if (user.TwoFactorToken != command.AuthToken || user.TwoFactorTokenExpiry < DateTime.UtcNow)
            return new Response<string>()
                .WithMessage("Error in verifying 2FA token")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Invalid or expired 2FA token.");

        // Verify 2FA token
        user.TwoFactorEnabled = true;
        user.TwoFactorToken = null!;
        user.TwoFactorTokenExpiry = null;
        var result = await _context.SaveChangesAsync(token) > 0;

         if(!result)
            return new Response<string>()
                .WithMessage("Error in verifying 2FA.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithData("Failed to verify 2FA.");

        return new Response<string>()
            .WithMessage("Success is 2FA verification.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("2FA verified successfully.");
    }
}
    
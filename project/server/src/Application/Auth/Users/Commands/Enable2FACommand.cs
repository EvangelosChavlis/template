// package
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record Enable2FACommand(Guid Id) : IRequest<Response<string>>;

public class Enable2FAHandler : IRequestHandler<Enable2FACommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public Enable2FAHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(Enable2FACommand command, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in enabling 2FA token")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");

        // Generate 2FA token
        var twoFactorToken = Guid.NewGuid().ToString().Substring(0, 6); // Example: a 6-digit token
        user.TwoFactorToken = twoFactorToken;
        user.TwoFactorTokenExpiry = DateTime.UtcNow.AddMinutes(10);

        var result = await _context.SaveChangesAsync(token) > 0;

        if (!result)
            return new Response<string>()
                .WithMessage("Error in enabling 2FA token.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to enable 2FA token.");

        // Send 2FA token via email (implement your email sending logic here)
        // Example: _emailService.SendTwoFactorTokenEmail(user.Email, twoFactorToken);

        return new Response<string>()
            .WithMessage("2FA token sent successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("2FA token sent successfully.");
    }
}
// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Helpers;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record ResetPasswordCommand(string Email, string AuthToken, string NewPassword) : IRequest<Response<string>>;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    private readonly IAuthHelper _authHelper;

    public ResetPasswordHandler(DataContext context, ICommonRepository commonRepository, 
        IAuthHelper authHelper)
    {
        _context = context;
        _commonRepository = commonRepository;
        _authHelper = authHelper;
    }

    public async Task<Response<string>> Handle(ResetPasswordCommand command, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Email == command.Email };
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error in reset password token.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");

        if (user.PasswordResetToken != command.AuthToken || user.PasswordResetTokenExpiry < DateTime.UtcNow)
            return new Response<string>()
                .WithMessage("Invalid or expired password reset token.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Invalid or expired password reset token.");

        // Reset password logic
        user.PasswordHash = _authHelper.HashPassword(command.NewPassword);
        user.PasswordResetToken = null!;
        user.PasswordResetTokenExpiry = null;
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            return new Response<string>()
                .WithMessage("Error in reset password token.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to reset password token.");

        return new Response<string>()
            .WithMessage("Password reset successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("Password reset successfully.");
    }
}
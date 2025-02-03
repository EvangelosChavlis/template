// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Users.Validators;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record ResetPasswordCommand(ResetPasswordDto Dto) : IRequest<Response<string>>;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommonQueries _commonQueries;

    public ResetPasswordHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork, 
        ICommonQueries commonQueries)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _commonQueries = commonQueries;
       
    }

    public async Task<Response<string>> Handle(ResetPasswordCommand command, CancellationToken token = default)
    {
        // Version Validation
        var versionValidationResult = UserValidators.Validate(command.Dto.VersionId);
        if (!versionValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(versionValidationResult.IsValid)
                .WithData(string.Join("\n", versionValidationResult.Errors));

        // Email Validation
        var emailValidationResult = UserValidators.ValidateEmail(command.Dto.Email);
        if (!emailValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(emailValidationResult.IsValid)
                .WithData(string.Join("\n", emailValidationResult.Errors));

        // AuthToken Validation
        var authValidationResult = UserValidators.ValidateEmail(command.Dto.Token);
        if (!authValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(authValidationResult.IsValid)
                .WithData(string.Join("\n", authValidationResult.Errors));

        // New Password Validation
        var passwordValidationResult = UserValidators.ValidateEmail(command.Dto.NewPassword);
        if (!passwordValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(passwordValidationResult.IsValid)
                .WithData(string.Join("\n", passwordValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);
                
        // Searching Item      
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Email == command.Dto.Email };
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in reset password token.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");
        }

        // Check for concurrency issues
        if (user.Version != command.Dto.VersionId)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("User has been modified by another user. Please try again.");
        }

        // Check if user is deactivated
        if (!user.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error confirming the user's email.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("User is deactivated.");
        }   
            
        // Check password token
        if (user.PasswordResetToken != command.Dto.Token || user.PasswordResetTokenExpiry < DateTime.UtcNow)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Invalid or expired password reset token.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Invalid or expired password reset token.");
        }
            

        // Reset password logic
        user.PasswordHash = await _commonQueries.HashPassword(command.Dto.NewPassword, token);
        user.PasswordResetToken = string.Empty;
        user.PasswordResetTokenExpiry = null;
        user.Version = Guid.NewGuid();

        // Validating, Saving Item
        var modelValidationResult = UserValidators.Validate(user);
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(user, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in reset password token.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to reset password token.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Password reset successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("Password reset successfully.");
    }
}
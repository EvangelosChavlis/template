// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Users.Validators;
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record Verify2FACommand(Guid Id, string AuthToken, Guid Version) : IRequest<Response<string>>;

public class Verify2FAHandler : IRequestHandler<Verify2FACommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;

    public Verify2FAHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(Verify2FACommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error in deactivating user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("Current user not found");

        // Id Validation
        var idValidationResult = command.Id.ValidateId();
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item 
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id };
        var user = await _commonRepository.GetResultByIdAsync(userFilters, token: token);

        // Check for existence
        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in verifying 2FA token")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User not found.");
        }

        // Check for concurrency issues
        if (user.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"This user has been modified by another {user.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Check for concurrency issues
        if (user.Version != command.Version)
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
                .WithMessage("Error in verifying 2FA token.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("User is deactivated.");
        }   
            
        // Check Token
        if (user.TwoFactorToken != command.AuthToken || user.TwoFactorTokenExpiry < DateTime.UtcNow)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in verifying 2FA token.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Invalid or expired 2FA token.");
        }
            
        // Verify 2FA token
        user.TwoFactorEnabled = true;
        user.TwoFactorToken = string.Empty;
        user.TwoFactorTokenExpiry = null;
        user.Version = Guid.NewGuid();

        // Validating, Saving Item
        var modelValidationResult = UserModelValidators.Validate(user);
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
                .WithMessage("Error in verifying 2FA.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithData("Failed to verify 2FA.");
        }

        // Unlock result
        var unlockResult = await _commonRepository.UnlockAsync<User>(user.Id, token);
        if(!unlockResult)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in verifying 2FA.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(unlockResult)
                .WithData("Failed to unlock user.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success is 2FA verification.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("2FA verified successfully.");
    }
}
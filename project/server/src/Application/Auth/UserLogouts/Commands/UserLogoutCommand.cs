// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Auth.UserLogouts.Mappings;
using server.src.Application.Auth.Users.Validators;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.UserLogouts.Commands;

public record UserLogoutCommand(Guid Id) : IRequest<Response<string>>;

public class UserLogoutHandler : IRequestHandler<UserLogoutCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserLogoutHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(UserLogoutCommand command, CancellationToken token = default)
    {
        // Id Validation
        var idValidationResult = UserLogoutValidators.Validate(command.Id);
        if (!idValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(idValidationResult.IsValid)
                .WithData(string.Join("\n", idValidationResult.Errors));

        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { u => u.Id == command.Id };
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Check for existence
        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("User with not found.");
        }

        // Check if the user account is active
        if (!user.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("User account is inactive.");
        }
            
        // Validating, Mapping, Saving
        user.SecurityStamp = Guid.NewGuid().ToString();
        var userModelValidationResult = UserValidators.Validate(user);
        if (!userModelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(userModelValidationResult.IsValid)
                .WithData(string.Join("\n", userModelValidationResult.Errors));
        }
        var result = await _commonRepository.UpdateAsync(user, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("An error occurred while updating the user login status. Please try again.");
        }

        // Mapping, Validating, Saving Item
        var userLogout = user.UserLogoutMapping();
        var userLogoutModelValidationResult = UserLogoutValidators.Validate(userLogout);
        if (!userLogoutModelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(userLogoutModelValidationResult.IsValid)
                .WithData(string.Join("\n", userLogoutModelValidationResult.Errors));
        }
        var resultUserLogout = await _commonRepository.AddAsync(userLogout, token);

        // Saving failed
        if (!resultUserLogout)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error in logging out user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("An error occurred while updating the user login status. Please try again.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage($"User {user.UserName} logged out successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData($"User {user.UserName} logged out successfully.");
    }
}

// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.UserRoles.Interfaces;
using server.src.Application.Auth.Users.Validators;
using server.src.Application.Helpers;
using server.src.Application.Interfaces;
using server.src.Application.Users.Mappings;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record RegisterUserCommand(UserDto Dto, bool Registered) : IRequest<Response<string>>;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthHelper _authHelper;
    private readonly IUserRoleCommands _userRoleCommands;

    public RegisterUserHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork, 
        IAuthHelper authHelper, IUserRoleCommands userRoleCommands)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _authHelper = authHelper;
        _userRoleCommands = userRoleCommands;
    }

    public async Task<Response<string>> Handle(RegisterUserCommand command, CancellationToken token = default)
    {
        // Dto Validation
        var dtoValidationResult = UserValidators.Validate(command.Dto);
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var emailIncludes = new Expression<Func<User, object>>[] {  };
        var emailFilters = new Expression<Func<User, bool>>[] { x => x.Email!.Equals(command.Dto.Email)};
        var existingEmail = await _commonRepository.GetResultByIdAsync(emailFilters, emailIncludes, token);

        // Check if user with this email already exists in the system
        if (existingEmail is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"User with email {existingEmail.Email} already exists.");
        }

        // Searching Item for existing username
        var userNameIncludes = new Expression<Func<User, object>>[] {  };
        var userNameFilters = new Expression<Func<User, bool>>[] { x => x.UserName!.Equals(command.Dto.UserName)};
        var existingUserName = await _commonRepository.GetResultByIdAsync(userNameFilters, userNameIncludes, token);

        // Check if user with this username already exists in the system
        if (existingUserName is not null)
        {
await _unitOfWork.RollbackTransactionAsync(token);            
            return new Response<string>()
                .WithMessage("Error creating user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"User with username {existingUserName.UserName} already exists.");
        }
            
        // Hash the password
        var passwordHash = _authHelper.HashPassword(command.Dto.Password);

        // Mapping and Saving User
        var user = command.Dto.CreateUserModelMapping(command.Registered, passwordHash);
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
        var result = await _commonRepository.AddAsync(user, token);

        // Saving failed
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error registering user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to register user.");
        }
            
        // Searching for the Role (e.g., 'User' role)
        var roleIncludes = new Expression<Func<Role, object>>[] {  };
        var roleFilters = new Expression<Func<Role, bool>>[] { x => x.Name!.Equals("User")};
        var role = await _commonRepository.GetResultByIdAsync(roleFilters, roleIncludes, token);

        // Check for existence
        if (role is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error registering user.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Role with name 'User' not found.");
        }

        // Assigning role to the user
        var assignResult = await _userRoleCommands.AssignRoleToUserAsync(user.Id, role.Id);
        if (!assignResult.Success)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error registering user.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(assignResult.Success)
                .WithData(assignResult.Data!);
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success in registering user.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"User {user.UserName} registered successfully.");
    }
}
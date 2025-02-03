// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Auth.UserLogins.Mappings;
using server.src.Application.Auth.Users.Validators;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.UserLogins.Commands;

public record UserLoginCommand(UserLoginDto Dto) : IRequest<Response<AuthenticatedUserDto>>;

public class UserLoginHandler : IRequestHandler<UserLoginCommand, Response<AuthenticatedUserDto>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICommonQueries _commonQueries;
    private readonly ICommonCommands _commonCommands;

    public UserLoginHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork, 
        ICommonQueries commonQueries, ICommonCommands commonCommands)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _commonQueries = commonQueries;
        _commonCommands = commonCommands;
    }

    public async Task<Response<AuthenticatedUserDto>> Handle(UserLoginCommand command, CancellationToken token = default)
    {
        // Dto Validation
        var dtoValidationResult = UserLoginValidators.Validate(command.Dto);
        if (!dtoValidationResult.IsValid)
            return new Response<AuthenticatedUserDto>()
                .WithMessage(string.Join("\n", dtoValidationResult.Errors))
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(new AuthenticatedUserDto("", ""));

        // Begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { u => u.UserName!.Equals(command.Dto.Username) };
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        // Check for existence
        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage($"User with username {command.Dto.Username} not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        // Check if the user account is active
        if (!user.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage("User account is inactive.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }
            

        // Check if user account is locked
        if (user.LockoutEnabled && user.LockoutEnd > DateTimeOffset.UtcNow)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage("Your account is locked due to multiple failed login attempts. Please try again later.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        var checkPassword = await _commonQueries.VerifyPassword(command.Dto.Password, user.PasswordHash, token);

        if (!checkPassword)
        {
            // Increment failed login attempts
            user.AccessFailedCount++;

            // Lock the account if failed attempts exceed the threshold
            if (user.AccessFailedCount >= 5)
            {
                user.LockoutEnd = DateTimeOffset.UtcNow.AddMinutes(15); // Lock the account for 15 minutes
                user.LockoutEnabled = true;
            }

            var modelValidationResult = UserValidators.Validate(user);
            if (!modelValidationResult.IsValid)
            {
                await _unitOfWork.RollbackTransactionAsync(token);
                return new Response<AuthenticatedUserDto>()
                    .WithMessage(string.Join("\n", modelValidationResult.Errors))
                    .WithStatusCode((int)HttpStatusCode.BadRequest)
                    .WithSuccess(modelValidationResult.IsValid)
                    .WithData(new AuthenticatedUserDto("", ""));
            }
            var result = await _commonRepository.UpdateAsync(user, token);

            // Saving failed
            if(!result)
            {
                await _unitOfWork.RollbackTransactionAsync(token);
                return new Response<AuthenticatedUserDto>()
                    .WithMessage("Error in logging account.")
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithSuccess(false)
                    .WithData(new AuthenticatedUserDto("", ""));
            }
            
            // Commit Transaction
            await _unitOfWork.CommitTransactionAsync(token);

            // Initializing object
            return new Response<AuthenticatedUserDto>()
                .WithMessage("Invalid password.\nYour account will be locked after 5 unsuccessful attempts.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(result)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        // Check if the user account is active
        if (!user.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage("User account is not active.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        if (user.AccessFailedCount is not 0)
        {
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            

            var modelValidationResult = UserValidators.Validate(user);
            if (!modelValidationResult.IsValid)
            {
                await _unitOfWork.RollbackTransactionAsync(token);
                return new Response<AuthenticatedUserDto>()
                    .WithMessage(string.Join("\n", modelValidationResult.Errors))
                    .WithStatusCode((int)HttpStatusCode.BadRequest)
                    .WithSuccess(modelValidationResult.IsValid)
                    .WithData(new AuthenticatedUserDto("", ""));
            }
            var result = await _commonRepository.UpdateAsync(user, token);

            // Saving failed
            if(!result)
            {
                await _unitOfWork.RollbackTransactionAsync(token);
                return new Response<AuthenticatedUserDto>()
                    .WithMessage("Error in logging account.")
                    .WithStatusCode((int)HttpStatusCode.InternalServerError)
                    .WithSuccess(false)
                    .WithData(new AuthenticatedUserDto("", ""));
            }
        }
       
        // Mapping, Validating, Saving Items
        var userLogin = user.UserLoginMapping();
        var userLoginValidationResult = UserLoginValidators.Validate(userLogin);
        if (!userLoginValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage(string.Join("\n", userLoginValidationResult.Errors))
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(userLoginValidationResult.IsValid)
                .WithData(new AuthenticatedUserDto("", ""));
        }
        var resultUserLogin = await _commonRepository.AddAsync(user, token);

        // Saving failed
        if (!resultUserLogin)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage("An error occurred while updating the user login status. Please try again.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData(new AuthenticatedUserDto("", ""));
        }

            
        // Generate JWT token
        var authToken = await _commonCommands.GenerateJwtToken(user.Id, user.UserName, 
            user.Email, user.SecurityStamp, token);

        // Generating failed
        if (!authToken.Success)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<AuthenticatedUserDto>()
                .WithMessage("An error occurred while generating token. Please try again.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(authToken.Success)
                .WithData(new AuthenticatedUserDto("", ""));
        }

        // Map user and token to DTO
        var resultDto = user.UserName!.AuthenticatedUserDtoMapping(authToken.Data!);

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<AuthenticatedUserDto>()
            .WithMessage("Login successful.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(resultDto);
    }
}
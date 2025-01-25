// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Models.Auth;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Auth.Users.Commands;

public record ConfirmEmailUserCommand(Guid Id) : IRequest<Response<string>>;

public class ConfirmEmailUserHandler : IRequestHandler<ConfirmEmailUserCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Guid> _validator;

    public ConfirmEmailUserHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork, 
        IValidator<Guid> validator)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Response<string>> Handle(ConfirmEmailUserCommand command, CancellationToken token = default)
    {
        // validation
        var validationResult = _validator.Validate(command.Id);

        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { u => u.Id == command.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);

            return new Response<string>()
                .WithMessage("Error confirming the user's email.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {command.Id} not found.");
        }
            

        if (user.EmailConfirmed)
        {
            await _unitOfWork.RollbackTransactionAsync(token);

            return new Response<string>()
                .WithMessage("Error confirming the user's email.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Email is confirmed.");
        }
            
        // Saving Item
        user.EmailConfirmed = true;
        var result = await _commonRepository.UpdateAsync(user, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);

            return new Response<string>()
                .WithMessage("Error confirming the user's email.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to confirm email.");
        }

        await _unitOfWork.CommitTransactionAsync(token);
            
        return new Response<string>()
            .WithMessage("Success confirming the user's email.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Email {user.Email} confirmed successfully");
    }
}
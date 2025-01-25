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

public record ConfirmPhoneNumberUserCommand(Guid Id) : IRequest<Response<string>>;

public class ConfirmPhoneNumberUserHandler : IRequestHandler<ConfirmPhoneNumberUserCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Guid> _validator;

    public ConfirmPhoneNumberUserHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork, 
        IValidator<Guid> validator)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Response<string>> Handle(ConfirmPhoneNumberUserCommand command, CancellationToken token = default)
    {
        // validation
        var validationResult = _validator.Validate(command.Id);

        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // begin transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == command.Id};
        var user = await _commonRepository.GetResultByIdAsync(userFilters, userIncludes, token);

        if (user is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);

            return new Response<string>()
                .WithMessage("Error confirming the user's phone number.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with id {command.Id} not found.");
        }
            
        if (user.PhoneNumberConfirmed)
        {
            await _unitOfWork.RollbackTransactionAsync(token);

            return new Response<string>()
                .WithMessage("Error confirming the user's phone number.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Phone number is confirmed.");

        }
            
        user.PhoneNumberConfirmed = true;
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            return new Response<string>()
                .WithMessage("Error confirming the user's phone number.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to confirm phone number.");
    
        return new Response<string>()
            .WithMessage("Success in confirming the user's phone number.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Phone number {user.PhoneNumber} confirmed successfully");
    }
}
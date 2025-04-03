// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.NaturalFeatures.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.NaturalFeatures.Commands;

public record DeleteNaturalFeatureCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeleteNaturalFeatureHandler : IRequestHandler<DeleteNaturalFeatureCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteNaturalFeatureHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork
    )
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeleteNaturalFeatureCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deleting natural feature.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(currentUser.UserFound)
                .WithData("Current user not found");


        // Validation
        var validationResult = command.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<string>()
                .WithMessage("Validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(validationResult.IsValid)
                .WithData(string.Join("\n", validationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<NaturalFeature, bool>>[] { t => t.Id == command.Id};
        var NaturalFeature = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (NaturalFeature is null)
            return new Response<string>()
                .WithMessage("Error deleting natural feature.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Natural feature not found.");

        // Check for concurrency issues
        if (NaturalFeature.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The natural feature has been modified by another user. Please try again.");
        }

        // Check if natural feature is already active
        if (NaturalFeature.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deleting natural feature.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Natural feature is active.");
        }

        // Check for concurrency issues
        if (NaturalFeature.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The natural feature has been modified by another {NaturalFeature.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Deleting Item
        var result = await _commonRepository.DeleteAsync(NaturalFeature, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deleting natural feature.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to delete natural feature.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success deleting natural feature.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Natural feature {NaturalFeature.Name} deleted successfully!");
    }
}
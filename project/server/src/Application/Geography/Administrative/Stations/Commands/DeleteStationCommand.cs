// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Common.Extensions;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Stations.Commands;

public record DeleteStationCommand(Guid Id, Guid Version) : IRequest<Response<string>>;

public class DeleteStationHandler : IRequestHandler<DeleteStationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly ICommonQueries _commonQueries;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteStationHandler(ICommonRepository commonRepository, 
        ICommonQueries commonQueries, IUnitOfWork unitOfWork
    )
    {
        _commonRepository = commonRepository;
        _commonQueries = commonQueries;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(DeleteStationCommand command, CancellationToken token = default)
    {
        // Check current user
        var currentUser = await _commonQueries.GetCurrentUser(token);
        if(!currentUser.UserFound)
            return new Response<string>()
                .WithMessage("Error deleting station.")
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
        var filters = new Expression<Func<Station, bool>>[] { s => s.Id == command.Id};
        var station = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (station is null)
            return new Response<string>()
                .WithMessage("Error deleting Station.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Station not found.");

        // Check for concurrency issues
        if (station.Version != command.Version)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Concurrency conflict.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData("The station has been modified by another user. Please try again.");
        }

        // Check if station is already active
        if (station.IsActive)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deleting station.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData("Station is active.");
        }

        // Check for concurrency issues
        if (station.IsLockedByOtherUser(currentUser.Id))
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity is currently locked.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData(@$"The station has been modified by another {station.LockedByUser!.UserName}. 
                    Please try again.");
        }

        // Deleting Item
        var result = await _commonRepository.DeleteAsync(station, token);

        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error deleting Station.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to delete Statione.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);
            
        // Initializing object
        return new Response<string>()
            .WithMessage("Success deleting station.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Station {station.Name} deleted successfully!");
    }
}
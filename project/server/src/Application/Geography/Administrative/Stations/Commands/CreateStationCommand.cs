// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Stations.Mappings;
using server.src.Application.Geography.Administrative.Stations.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Stations.Commands;

public record CreateStationCommand(CreateStationDto Dto) : IRequest<Response<string>>;

public class CreateStationHandler : IRequestHandler<CreateStationCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStationHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateStationCommand command, CancellationToken token = default)
    {
        // Dto Validation
        var dtoValidationResult = command.Dto.Validate();
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<Station, bool>>[] 
        { 
            s => s.Name!.Equals(command.Dto.Name) ||
                s.Code == command.Dto.Code
        };
        var existingStation = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the station already exists in the system
        if (existingStation is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating station.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Station with name {existingStation.Name} already exists.");
        }

        // Searching Item
        var locationFilters = new Expression<Func<Location, bool>>[] 
        { 
            l => l.Latitude == command.Dto.Latitude && 
                l.Longitude == command.Dto.Longitude
        };
        var location = await _commonRepository.GetResultByIdAsync(locationFilters, token: token);

        // Check for existence
        if (location is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating location.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Location not found.");
        }

        // Searching Item
        var neighborhoodFilters = new Expression<Func<Neighborhood, bool>>[] { n => n.Id == command.Dto.NeighborhoodId };
        var neighborhood = await _commonRepository.GetResultByIdAsync(neighborhoodFilters, token: token);

        // Mapping and Saving Station
        var station = command.Dto.CreateStationModelMapping(location, neighborhood);
        var modelValidationResult = station.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(station, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating station.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create station.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating station.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Station {station.Name} inserted successfully!");
    }
}
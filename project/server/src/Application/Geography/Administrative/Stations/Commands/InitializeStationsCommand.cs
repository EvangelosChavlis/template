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

public record InitializeStationsCommand(List<CreateStationDto> Dto) : IRequest<Response<string>>;

public class InitializeStationsHandler : IRequestHandler<InitializeStationsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeStationsHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeStationsCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var stations = new List<Station>();
        var existingCodes = new HashSet<string>();
        
        var failures = new List<string>();

        foreach (var item in command.Dto)
        {
            // Dto Validation
            var dtoValidationResult = item.Validate();
            if (!dtoValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", dtoValidationResult.Errors));
                continue;
            }

           // Searching Item
            var filters = new Expression<Func<Station, bool>>[] 
            { 
                s => s.Name!.Equals(item.Name) ||
                    s.Code == item.Code
            };
            var existingStation = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the station already exists in the system
            if (existingStation is not null)
            {
                failures.Add($"Station with name {existingStation.Name} already exists.");
                continue;
            }

             // Searching Item
            var locationFilters = new Expression<Func<Location, bool>>[] 
            { 
                l => l.Latitude == item.Latitude && 
                    l.Longitude == item.Longitude
            };
            var location = await _commonRepository.GetResultByIdAsync(locationFilters, token: token);

            // Check for existence
            if (location is null)
            {
                failures.Add("Location not found.");
                continue;
            }
            // Searching Item
            var neighborhoodFilters = new Expression<Func<Neighborhood, bool>>[] { n => n.Id == item.NeighborhoodId };
            var neighborhood = await _commonRepository.GetResultByIdAsync(neighborhoodFilters, token: token);

            // Mapping and Saving Station
            var station = item.CreateStationModelMapping(location, neighborhood);
            var modelValidationResult = station.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(station.Code))
            {
                failures.Add($"Code {station.Code} already exists in the list of stations.");
                continue;
            }

            stations.Add(station);
            existingCodes.Add(station.Code);
        }

        var result = await _commonRepository.AddRangeAsync(stations, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing stations.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize stations.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing stations.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{stations.Count}/{command.Dto.Count} stations inserted successfully!");
    }
}

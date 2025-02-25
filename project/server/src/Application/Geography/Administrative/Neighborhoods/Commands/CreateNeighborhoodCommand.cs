// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Neighborhoods.Mappings;
using server.src.Application.Geography.Administrative.Neighborhoods.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Commands;

public record CreateNeighborhoodCommand(CreateNeighborhoodDto Dto) : IRequest<Response<string>>;

public class CreateNeighborhoodHandler : IRequestHandler<CreateNeighborhoodCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNeighborhoodHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(CreateNeighborhoodCommand command, CancellationToken token = default)
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
        var filters = new Expression<Func<Neighborhood, bool>>[] { c => c.Name!.Equals(command.Dto.Name) };
        var existingNeighborhood = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check if the terrain type already exists in the system
        if (existingNeighborhood is not null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Neighborhood with name {existingNeighborhood.Name} already exists.");
        }

        // Searching Item
        var districtFilters = new Expression<Func<District, bool>>[] { d => d.Id == command.Dto.DistrictId };
        var district = await _commonRepository.GetResultByIdAsync(districtFilters, token: token);

        // Check for existence
        if (district is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("District not found.");
        }

        // Mapping and Saving Neighborhood
        var neighborhood = command.Dto.CreateNeighborhoodModelMapping(district);
        var modelValidationResult = neighborhood.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(neighborhood, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating neighborhood.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create neighborhood.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating neighborhood.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Neighborhood {neighborhood.Name} inserted successfully!");
    }
}
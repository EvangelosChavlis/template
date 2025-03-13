// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Continents.Mappings;
using server.src.Application.Geography.Administrative.Continents.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Continents.Commands;

public record InitializeContinentsCommand(List<CreateContinentDto> Dto) : IRequest<Response<string>>;

public class InitializeContinentsHandler : IRequestHandler<InitializeContinentsCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeContinentsHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeContinentsCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var continents = new List<Continent>();
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
            var filters = new Expression<Func<Continent, bool>>[] 
            {
                c => c.Name!.Equals(item.Name) || 
                    c.Code!.Equals(item.Code)
            };
            var existingContinent = await _commonRepository.GetResultByIdAsync(
                filters, token: token);

            // Check if the continent name already exists in the system
            if (existingContinent is not null)
            {
                failures.Add($"Continent with name {existingContinent.Name} already exists.");
                continue;
            } 
            
            // Mapping and Saving Continent
            var continent = item.CreateContinentModelMapping();
            var modelValidationResult = continent.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }
                
            if (existingCodes.Contains(continent.Code))
            {
                failures.Add($"Code {continent.Code} already exists in the list of continents.");
                continue;
            }
            
            continents.Add(continent);
            existingCodes.Add(continent.Code);
        }

        var result = await _commonRepository.AddRangeAsync(continents, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing continents.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize continents.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing continent.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{continents.Count}/{command.Dto.Count} continents inserted successfully!");
    }
}
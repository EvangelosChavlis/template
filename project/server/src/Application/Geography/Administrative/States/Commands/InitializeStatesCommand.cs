// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.States.Mappings;
using server.src.Application.Geography.Administrative.States.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.States.Commands;

public record InitializeStatesCommand(List<CreateStateDto> Dto) : IRequest<Response<string>>;

public class InitializeStatesHandler : IRequestHandler<InitializeStatesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeStatesHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeStatesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var states = new List<State>();
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
            var filters = new Expression<Func<State, bool>>[] 
            { 
                s => s.Name!.Equals(item.Name) ||
                    s.Code == item.Code
            };
            var existingState = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the state already exists in the system
            if (existingState is not null)
            {
                failures.Add($"State with name {existingState.Name} already exists.");
                continue;
            }

            // Searching Item
            var countryFilters = new Expression<Func<Country, bool>>[] { c => c.Id == item.CountryId };
            var country = await _commonRepository.GetResultByIdAsync(countryFilters, token: token);

            // Check for existence
            if (country is null)
            {
                failures.Add("Country not found.");
                continue;
            }

            // Mapping and Saving State
            var state = item.CreateStateModelMapping(country);
            var modelValidationResult = state.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(state.Code))
            {
                failures.Add($"Code {state.Code} already exists in the list of states.");
                continue;
            }

            states.Add(state);
            existingCodes.Add(state.Code);
        }

        var result = await _commonRepository.AddRangeAsync(states, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing state.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize state.");
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing states.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{states.Count}/{command.Dto.Count} states inserted successfully!");
    }
}

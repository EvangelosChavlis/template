// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Administrative.Countries.Mappings;
using server.src.Application.Geography.Administrative.Countries.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Countries.Commands;

public record InitializeCountriesCommand(List<CreateCountryDto> Dto) : IRequest<Response<string>>;

public class InitializeCountriesHandler : IRequestHandler<InitializeCountriesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeCountriesHandler(ICommonRepository commonRepository, 
        IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeCountriesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var countries = new List<Country>();
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
            var filters = new Expression<Func<Country, bool>>[] 
            { 
                c => c.Name!.Equals(item.Name) || 
                    c.Code!.Equals(item.Code)
            };
            var existingCountry = await _commonRepository.GetResultByIdAsync(filters, token: token);

            // Check if the country already exists in the system
            if (existingCountry is not null)
            {
                failures.Add($"Country with name {existingCountry.Name} already exists.");
                continue;
            }
                
            // Searching Item
            var continentFilters = new Expression<Func<Continent, bool>>[] { c => c.Id == item.ContinentId };
            var continent = await _commonRepository.GetResultByIdAsync(continentFilters, token: token);

            // Check for existence
            if (continent is null)
            {
                failures.Add("Continent not found.");
                continue;
            }

            // Mapping and Saving Country
            var country = item.CreateCountryModelMapping(continent);
            var modelValidationResult = country.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }

            if (existingCodes.Contains(country.Code))
            {
                failures.Add($"Code {country.Code} already exists in the list of countries.");
                continue;
            }

            countries.Add(country);
            existingCodes.Add(country.Code);
        }
        var result = await _commonRepository.AddRangeAsync(countries, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing countries.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to initialize countries.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing countries.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithFailures(failures)
            .WithSuccess(result)
            .WithData($"{countries.Count} countries inserted successfully!");
    }
}
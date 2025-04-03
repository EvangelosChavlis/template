// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Timezones.Mappings;
using server.src.Application.Geography.Natural.Timezones.Validators;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Timezones.Commands;

public record InitializeTimezonesCommand(List<CreateTimezoneDto> Dto) : IRequest<Response<string>>;

public class InitializeTimezonesHandler : IRequestHandler<InitializeTimezonesCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InitializeTimezonesHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> Handle(InitializeTimezonesCommand command, CancellationToken token = default)
    {
        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        var timezones = new List<Timezone>();
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
            var filters = new Expression<Func<Timezone, bool>>[] 
            {
                c => c.Name!.Equals(item.Name) || 
                    c.Code!.Equals(item.Code)
            };
            var existingTimezone = await _commonRepository.GetResultByIdAsync(
                filters, token: token);

            // Check if the timezone name already exists in the system
            if (existingTimezone is not null)
            {
                failures.Add($"Timezone with name {existingTimezone.Name} already exists.");
                continue;
            } 
            
            // Mapping and Saving Timezone
            var timezone = item.CreateTimezoneModelMapping();
            var modelValidationResult = timezone.Validate();
            if (!modelValidationResult.IsValid)
            {
                failures.Add(string.Join("\n", modelValidationResult.Errors));
                continue;
            }
                
            if (existingCodes.Contains(timezone.Code))
            {
                failures.Add($"Code {timezone.Code} already exists in the list of timezones.");
                continue;
            }
            
            timezones.Add(timezone);
            existingCodes.Add(timezone.Code);
        }

        var result = await _commonRepository.AddRangeAsync(timezones, token);

        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error initializing timezones.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithFailures(failures)
                .WithData("Failed to initialize timezones.");
        }

        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success initializing timezones.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithFailures(failures)
            .WithData($"{timezones.Count}/{command.Dto.Count} timezones inserted successfully!");
    }
}

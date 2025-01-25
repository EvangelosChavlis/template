// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Forecasts.Commands;

public record CreateForecastCommand(ForecastDto Dto) : IRequest<Response<string>>;

public class CreateForecastHandler : IRequestHandler<CreateForecastCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    
    public CreateForecastHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(CreateForecastCommand command, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] {  };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == command.Dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token);

        if(warning is null)
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"Warning {command.Dto.WarningId} not found");

        // Mapping and Saving Item
        var forecast = command.Dto.CreateForecastModelMapping(warning);
        await _context.Forecasts.AddAsync(forecast, token);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            return new Response<string>()
                .WithMessage("Error creating forecast.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to create forecast.");

        return new Response<string>()
            .WithMessage("Success creating forecast.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} inserted successfully!");
    }
}
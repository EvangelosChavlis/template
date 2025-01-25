// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Forecasts.Commands;

public record DeleteForecastCommand(Guid Id) : IRequest<Response<string>>;

public class DeleteForecastHandler : IRequestHandler<DeleteForecastCommand, Response<string>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;
    
    public DeleteForecastHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<string>> Handle(DeleteForecastCommand command, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] { };
        var filters = new Expression<Func<Forecast, bool>>[] { x => x.Id == command.Id};
        var forecast = await _commonRepository.GetResultByIdAsync(_context.Forecasts, filters, includes, token);
        
        if (forecast is null)
            return new Response<string>()
                .WithMessage("Error deleting forecast.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData("Forecast not found.");

        // Deleting
        _context.Forecasts.Remove(forecast);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            return new Response<string>()
                .WithMessage("Error deleting forecast.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("Failed to delete forecast.");

        // Initializing object
        return new Response<string>()
            .WithMessage("Success deleting forecast.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} deleted successfully!");
    }
}
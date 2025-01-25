// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Forecasts.Queries;

public record GetForecastByIdQuery(Guid Id) : IRequest<Response<ItemForecastDto>>;

public class GetForecastByIdHandler : IRequestHandler<GetForecastByIdQuery, Response<ItemForecastDto>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public GetForecastByIdHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemForecastDto>> Handle(GetForecastByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] 
        { 
            f => f.Warning
        };
        var filters = new Expression<Func<Forecast, bool>>[] { x => x.Id == query.Id};
        var forecast = await _commonRepository.GetResultByIdAsync(_context.Forecasts, filters, includes, token);

        if (forecast is null)
            return new Response<ItemForecastDto>()
                .WithMessage("Forecast not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ForecastsMappings.ErrorItemForecastDtoMapping());

        // Mapping
        var dto = forecast.ItemForecastDtoMapping();

        forecast.IsRead = true;
        var result = await _context.SaveChangesAsync(token) > 0;

        if (!result)
            return new Response<ItemForecastDto>()
                .WithMessage("An error occurred while updating forecast's status. Please try again.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithData(ForecastsMappings.ErrorItemForecastDtoMapping());

        // Initializing object
        return new Response<ItemForecastDto>()
            .WithMessage("Forecast fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData(dto);
    }
}
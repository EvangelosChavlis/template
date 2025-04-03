// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.Collections.Forecasts.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Forecasts.Dtos;
using server.src.Domain.Weather.Collections.Forecasts.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.Forecasts.Queries;

public record GetForecastByIdQuery(Guid Id) : IRequest<Response<ItemForecastDto>>;

public class GetForecastByIdHandler : IRequestHandler<GetForecastByIdQuery, Response<ItemForecastDto>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetForecastByIdHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ItemForecastDto>> Handle(GetForecastByIdQuery query, CancellationToken token = default)
    {
        // validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemForecastDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemForecastDtoMapper
                    .ErrorItemForecastDtoMapping());

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] 
        { 
            f => f.Warning,
            f => f.Station,
            f => f.MoonPhase
        };
        var filters = new Expression<Func<Forecast, bool>>[] { x => x.Id == query.Id};
        var forecast = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);

        // Check for existence
        if (forecast is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<ItemForecastDto>()
                .WithMessage("Forecast not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemForecastDtoMapper
                    .ErrorItemForecastDtoMapping());
        }
            

        // Mapping
        var dto = forecast.ItemForecastDtoMapping();
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<ItemForecastDto>()
            .WithMessage("Forecast fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
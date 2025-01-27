// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Weather.Forecasts.Mappings;
using server.src.Application.Weather.Forecasts.Validators;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Forecasts.Queries;

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
        var validationResult = ForecastValidators.Validate(query.Id);
        if (!validationResult.IsValid)
            return new Response<ItemForecastDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ForecastsMappings.ErrorItemForecastDtoMapping());

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] 
        { 
            f => f.Warning
        };
        var filters = new Expression<Func<Forecast, bool>>[] { x => x.Id == query.Id};
        var forecast = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        // Check for existence
        if (forecast is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<ItemForecastDto>()
                .WithMessage("Forecast not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ForecastsMappings.ErrorItemForecastDtoMapping());
        }
            

        // Mapping
        var dto = forecast.ItemForecastDtoMapping();

        // Mapping, Validating, Saving Item
        forecast.IsRead = true;
        var modelValidationResult = ForecastValidators.Validate(forecast);
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<ItemForecastDto>()
                .WithMessage(string.Join("\n", modelValidationResult.Errors))
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(ForecastsMappings.ErrorItemForecastDtoMapping());
        }
        var result = await _commonRepository.UpdateAsync(forecast, token);
        
        // Saving failed.
        if (!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<ItemForecastDto>()
                .WithMessage("An error occurred while updating forecast's status. Please try again.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithData(ForecastsMappings.ErrorItemForecastDtoMapping());
        }
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<ItemForecastDto>()
            .WithMessage("Forecast fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(result)
            .WithData(dto);
    }
}
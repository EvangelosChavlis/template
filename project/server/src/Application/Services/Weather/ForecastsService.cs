// packages
using System.Linq.Expressions;
using System.Net;
using System.Text;

// source
using server.src.Application.Filters.Weather;
using server.src.Application.Includes.Weather;
using server.src.Application.Interfaces.Weather;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Exceptions;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Weather;

public class ForecastsService : IForecastsService
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public ForecastsService(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }


    public async Task<ListResponse<List<ListItemForecastDto>>> GetForecastsService(UrlQuery pageParams, CancellationToken token = default)
    {
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ForecastFilters.ForecastTempSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Forecast, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ForecastSearchFilter();

        var filters = new Expression<Func<Forecast, bool>>[] { filter! };

        // Including
        var includes = ForecastsIncludes.GetForecastsIncludes();

        // Paging
        var pagedForecasts = await _commonRepository.GetPagedResultsAsync(_context.Forecasts, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedForecasts.Rows.Select(w => w.ListItemForecastDtoMapping()).ToList();

        // Initializing object
        return new ListResponse<List<ListItemForecastDto>>()
        {
            Data = dto,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedForecasts.UrlQuery.TotalRecords,
                PageSize = pagedForecasts.UrlQuery.PageSize,
                PageNumber = pagedForecasts.UrlQuery.PageNumber ?? 1,
            }
        };
    }


    public async Task<ItemResponse<ItemForecastDto>> GetForecastByIdService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] 
        { 
            f => f.Warning
        };
        var filters = new Expression<Func<Forecast, bool>>[] { x => x.Id == id};
        var forecast = await _commonRepository.GetResultByIdAsync(_context.Forecasts, filters, includes, token) ?? 
            throw new CustomException("Forecast not found", (int)HttpStatusCode.NotFound);

        // Mapping
        var dto = forecast.ItemForecastDtoMapping();

        // Initializing object
        return new ItemResponse<ItemForecastDto>()
            .WithData(dto);
    }


    public async Task<CommandResponse<string>> CreateForecastService(ForecastDto dto, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] {  };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token) ?? 
            throw new CustomException($"Warning {dto.WarningId} not found", (int)HttpStatusCode.NotFound);

        // Mapping and Saving Node Status
        var forecast = dto.CreateForecastModelMapping(warning);
        await _context.Forecasts.AddAsync(forecast, token);

        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            throw new CustomException("Failed to create forecast.", (int)HttpStatusCode.BadRequest);

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} inserted successfully!");
    }


    public async Task<CommandResponse<string>> InitializeForecastsService(List<ForecastDto> dtos, CancellationToken token = default)
    {
        var result = false;
        var messageBuilder = new StringBuilder();

        foreach (var dto in dtos)
        {
            // Searching Item
            var includes = new Expression<Func<Warning, object>>[] {  };
            var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == dto.WarningId};
            var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token) ?? 
                throw new CustomException($"Warning not found", (int)HttpStatusCode.NotFound);

            // Mapping and Saving Node Status
            var forecast = dto.CreateForecastModelMapping(warning);
            await _context.Forecasts.AddAsync(forecast, token);

            result = await _context.SaveChangesAsync(token) > 0;

            if(!result)
                throw new CustomException("Failed to create forecast.", (int)HttpStatusCode.BadRequest);
        
             messageBuilder.AppendLine($"Forecast {forecast.Date.GetFullLocalDateTimeString()} inserted successfully!");
        }

        var message = messageBuilder.ToString().TrimEnd();

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData(message);
    }


    public async Task<CommandResponse<string>> UpdateForecastService(Guid id, ForecastDto dto, CancellationToken token = default)
    {
        // Searching Item (Warning)
        var warningIncludes = new Expression<Func<Warning, object>>[] {  };
        var warningFilters = new Expression<Func<Warning, bool>>[] { x => x.Id == dto.WarningId};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, warningFilters, warningIncludes, token) ?? 
            throw new CustomException("Warning not found", (int)HttpStatusCode.NotFound);

        // Searching Item (forecast)
        var forecastIncludes = new Expression<Func<Forecast, object>>[] {  };
        var forecastFilters = new Expression<Func<Forecast, bool>>[] { x => x.Id == id};
        var forecast = await _commonRepository.GetResultByIdAsync(_context.Forecasts, forecastFilters, forecastIncludes, token) ?? 
            throw new CustomException("Forecast not found", (int)HttpStatusCode.NotFound);

        // Mapping and Saving
        dto.UpdateForecastMapping(forecast, warning);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            throw new CustomException("Failed to update forecast.", (int)HttpStatusCode.BadRequest);

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} updated successfully!");
    }


    public async Task<CommandResponse<string>> DeleteForecastService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Forecast, object>>[] { };
        var filters = new Expression<Func<Forecast, bool>>[] { x => x.Id == id};
        var forecast = await _commonRepository.GetResultByIdAsync(_context.Forecasts, filters, includes, token) ?? 
            throw new CustomException("Forecast not found", (int)HttpStatusCode.NotFound);

        // Deleting
        _context.Forecasts.Remove(forecast);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            throw new CustomException("Failed to delete forecast.", (int)HttpStatusCode.BadRequest);

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Forecast {forecast.Date.GetLocalDateString()} deleted successfully!");
    }
}
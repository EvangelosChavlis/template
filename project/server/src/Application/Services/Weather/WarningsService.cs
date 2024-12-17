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
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Weather;

public class WarningsService : IWarningsService
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public WarningsService(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }


    public async Task<ListResponse<List<ListItemWarningDto>>> GetWarningsService(UrlQuery pageParams, CancellationToken token = default)
    {
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = WarningFilters.WarningNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Warning, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.WarningSearchFilter();

        var filters = new Expression<Func<Warning, bool>>[] { filter! };

        // Including
        var includes = WarningsIncludes.GetWarningsIncludes();

        // Paging
        var pagedWarnings = await _commonRepository.GetPagedResultsAsync(_context.Warnings, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedWarnings.Rows.Select(w => w.ListItemWarningDtoMapping()).ToList();

        // Initializing object
        return new ListResponse<List<ListItemWarningDto>>()
        {
            Data = dto,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedWarnings.UrlQuery.TotalRecords,
                PageSize = pagedWarnings.UrlQuery.PageSize,
                PageNumber = pagedWarnings.UrlQuery.PageNumber ?? 1,
            }
        };
    }

    public async Task<ItemResponse<List<PickerWarningDto>>> GetWarningsPickerService(CancellationToken token = default)
    {
        var warnings = await _commonRepository.GetResultPickerAsync(_context.Warnings, token);

        var dto = warnings.Select(o => o.PickerWarningDtoMapping()).ToList();

        return new ItemResponse<List<PickerWarningDto>>().WithData(dto);
    }

    public async Task<ItemResponse<ItemWarningDto>> GetWarningByIdService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] 
        { 
            w => w.Forecasts
        };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == id};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token) ?? 
            throw new CustomException("Warning not found", (int)HttpStatusCode.NotFound);

        // Mapping
        var dto = warning.ItemWarningDtoMapping();

        // Initializing object
        return new ItemResponse<ItemWarningDto>()
            .WithData(dto);
    }

    public async Task<CommandResponse<string>> InitializeWarningsService(List<WarningDto> dtos, CancellationToken token = default)
    {
        var result = false;
        var messageBuilder = new StringBuilder();

        foreach (var dto in dtos)
        {
            // Mapping and Saving Node Status
            var warning = dto.CreateWarningModelMapping();
            await _context.Warnings.AddAsync(warning, token);

            result = await _context.SaveChangesAsync(token) > 0;

            if(!result)
                throw new CustomException("Failed to create warning.", (int)HttpStatusCode.BadRequest);

            messageBuilder.AppendLine($"Warning {warning.Name} inserted successfully!");
        }

        var message = messageBuilder.ToString().TrimEnd();

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData(message);
    }

    public async Task<CommandResponse<string>> CreateWarningService(WarningDto dto, CancellationToken token = default)
    {
        // Mapping and Saving Node Status
        var warning = dto.CreateWarningModelMapping();
        await _context.Warnings.AddAsync(warning, token);

        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            throw new CustomException("Failed to create warning.", (int)HttpStatusCode.BadRequest);

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} inserted successfully!");
    }

    public async Task<CommandResponse<string>> UpdateWarningService(Guid id, WarningDto dto, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] {  };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == id};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token) ?? 
            throw new CustomException("Warning not found", (int)HttpStatusCode.NotFound);

        // Mapping and Saving
        dto.UpdateWarningMapping(warning);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            throw new CustomException("Failed to update warning.", (int)HttpStatusCode.BadRequest);

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} updated successfully!");
    }



    public async Task<CommandResponse<string>> DeleteWarningService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] { };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == id};
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token) ?? 
            throw new CustomException("Warning not found", (int)HttpStatusCode.NotFound);

        // Deleting
        _context.Warnings.Remove(warning);
        var result = await _context.SaveChangesAsync(token) > 0;

        if(!result)
            throw new CustomException("Failed to delete warning.", (int)HttpStatusCode.BadRequest);

        // Initializing object
        return new CommandResponse<string>()
            .WithSuccess(result)
            .WithData($"Warning {warning.Name} deleted successfully!");
    }
}
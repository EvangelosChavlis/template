// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Filters.Metrics;
using server.src.Application.Includes.Weather;
using server.src.Application.Interfaces.Metrics;
using server.src.Application.Mappings.Metrics;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Exceptions;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Metrics;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Metrics;

public class TelemetryService : ITelemetryService
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public TelemetryService(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryService(UrlQuery pageParams, CancellationToken token = default)
    {
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TelemetryFiltrers.TelemetryNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Telemetry, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TelemetrySearchFilter();

        var filters = new Expression<Func<Telemetry, bool>>[] { filter! };

        // Including
        var includes = TelemetryIncludes.GetTelemetryIncludes();

        // Paging
        var pagedTelemetry = await _commonRepository.GetPagedResultsAsync(_context.TelemetryRecords, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedTelemetry.Rows.Select(t => t.ListItemTelemetryDtoMapping()).ToList();

        // Initializing object
        return new ListResponse<List<ListItemTelemetryDto>>()
        {
            Data = dto,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTelemetry.UrlQuery.TotalRecords,
                PageSize = pagedTelemetry.UrlQuery.PageSize,
                PageNumber = pagedTelemetry.UrlQuery.PageNumber ?? 1,
            }
        };
    }

    public async Task<ItemResponse<ItemTelemetryDto>> GetTelemetryByIdService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Telemetry, object>>[] { };
        var filters = new Expression<Func<Telemetry, bool>>[] { x => x.Id == id};
        var telemetry = await _commonRepository.GetResultByIdAsync(_context.TelemetryRecords, filters, includes, token) ?? 
            throw new CustomException("Telemetry not found", (int)HttpStatusCode.NotFound);

        // Mapping
        var dto = telemetry.ItemTelemetryDtoMapping();

        // Initializing object
        return new ItemResponse<ItemTelemetryDto>()
            .WithData(dto);
    }
}
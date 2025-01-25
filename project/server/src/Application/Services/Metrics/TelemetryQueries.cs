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
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Metrics;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Metrics;

public class TelemetryQueries : ITelemetryQueries
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public TelemetryQueries(DataContext context, ICommonRepository commonRepository)
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

        // Determine success 
        var success = pagedTelemetry.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemTelemetryDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Telemetry data fetched successfully." : 
                "No telemetry data found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTelemetry.UrlQuery.TotalRecords,
                PageSize = pagedTelemetry.UrlQuery.PageSize,
                PageNumber = pagedTelemetry.UrlQuery.PageNumber ?? 1,
            }
        };
    }

    public async Task<ListResponse<List<ListItemTelemetryDto>>> GetTelemetryByUserIdService(Guid id, UrlQuery pageParams, CancellationToken token = default)
    {
        var userIncludes = new Expression<Func<User, object>>[] { };
        var userFilters = new Expression<Func<User, bool>>[] { x => x.Id == id};
        var user = await _commonRepository.GetResultByIdAsync(_context.Users, userFilters, userIncludes, token);

        if (user is null)
            return new ListResponse<List<ListItemTelemetryDto>>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData([]);
                
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = TelemetryFiltrers.TelemetryNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<Telemetry, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.TelemetrySearchFilter();

        var filters = filter.TelemetryMatchFilters(user.Id);

        // Including
        var includes = TelemetryIncludes.GetTelemetryIncludes();

        // Paging
        var pagedTelemetry = await _commonRepository.GetPagedResultsAsync(_context.TelemetryRecords, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedTelemetry.Rows.Select(t => t.ListItemTelemetryDtoMapping()).ToList();

        // Determine success 
        var success = pagedTelemetry.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemTelemetryDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Telemetry data fetched successfully." : 
                "No telemetry data found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedTelemetry.UrlQuery.TotalRecords,
                PageSize = pagedTelemetry.UrlQuery.PageSize,
                PageNumber = pagedTelemetry.UrlQuery.PageNumber ?? 1,
            }
        };
    }

    public async Task<Response<ItemTelemetryDto>> GetTelemetryByIdService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Telemetry, object>>[] { x => x.User };
        var filters = new Expression<Func<Telemetry, bool>>[] { x => x.Id == id};
        var telemetry = await _commonRepository.GetResultByIdAsync(_context.TelemetryRecords, filters, includes, token);

        if (telemetry is null)
            return new Response<ItemTelemetryDto>()
                .WithMessage("Telemetry not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(TelemetryMappings.ErrorItemTelemetryDtoMapping());

        // Mapping
        var dto = telemetry.ItemTelemetryDtoMapping();

        // Initializing object
          return new Response<ItemTelemetryDto>()
            .WithMessage("Telemetry fetched successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
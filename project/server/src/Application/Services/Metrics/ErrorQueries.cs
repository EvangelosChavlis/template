// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Filters.Metrics;
using server.src.Application.Includes.Metrics;
using server.src.Application.Interfaces.Metrics;
using server.src.Application.Mappings.Metrics;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Common;
using server.src.Domain.Models.Errors;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Services.Metrics;

public class ErrorQueries : IErrorQueries
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public ErrorQueries(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }


    public async Task<ListResponse<List<ListItemErrorDto>>> GetErrorsService(UrlQuery pageParams, CancellationToken token = default)
    {
        // Default Sorting
        if (!pageParams.HasSortBy)
        {
            pageParams.SortBy = ErrorsFiltrers.ErrorNameSorting;
            pageParams.SortDescending = false;
        }

        // Filtering
        Expression<Func<LogError, bool>>? filter = null;
        if (pageParams.HasFilter) filter = pageParams.Filter!.ErrorSearchFilter();

        var filters = new Expression<Func<LogError, bool>>[] { filter! };

        // Including
        var includes = ErrorsIncludes.GetErrorsIncludes();

        // Paging
        var pagedErrors = await _commonRepository.GetPagedResultsAsync(_context.LogErrors, pageParams, filters, includes, token);
        // Mapping
        var dto = pagedErrors.Rows.Select(e => e.ListItemErrorDtoMapping()).ToList();

        // Determine success 
        var success = pagedErrors.Rows.Count > 0;

        // Initializing object
        return new ListResponse<List<ListItemErrorDto>>()
        {
            Data = dto,
            Success = success,
            Message = success ? "Errors fetched successfully." : 
                "No errors found matching the filter criteria.",
            StatusCode = success ? (int)HttpStatusCode.OK : 
                (int)HttpStatusCode.NotFound,
            Pagination = new PaginatedList
            {
                TotalRecords = pagedErrors.UrlQuery.TotalRecords,
                PageSize = pagedErrors.UrlQuery.PageSize,
                PageNumber = pagedErrors.UrlQuery.PageNumber ?? 1,
            }
        };
    }

    public async Task<Response<ItemErrorDto>> GetErrorByIdService(Guid id, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<LogError, object>>[] { };
        var filters = new Expression<Func<LogError, bool>>[] { x => x.Id == id};
        var warning = await _commonRepository.GetResultByIdAsync(_context.LogErrors, filters, includes, token);

        if (warning is null)
            return new Response<ItemErrorDto>()
                .WithMessage("Error not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorsMappings.ItemErrorDtoMapping());

        // Mapping
        var dto = warning.ItemErrorDtoMapping();

        // Initializing object
        return new Response<ItemErrorDto>()
            .WithMessage("Error fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
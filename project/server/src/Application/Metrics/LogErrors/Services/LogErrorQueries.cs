// source
using server.src.Application.Metrics.LogErrors.Interfaces;
using server.src.Application.Metrics.LogErrors.Queries;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.LogErrors.Dtos;
using server.src.Domain.Common.Models;
using server.src.Application.Common.Services;

namespace server.src.Application.Metrics.Errors.Services;

public class LogErrorQueries : ILogErrorQueries
{
    private readonly RequestExecutor _requestExecutor;

    public LogErrorQueries(RequestExecutor requestExecutor)
    {
        _requestExecutor = requestExecutor;
    }

    public async Task<ListResponse<List<ListItemLogErrorDto>>> GetLogErrorsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetLogErrorsQuery(urlQuery);
        return await _requestExecutor.Execute<GetLogErrorsQuery, ListResponse<List<ListItemLogErrorDto>>>(query, token);
    }

    public async Task<Response<ItemLogErrorDto>> GetLogErrorByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetLogErrorByIdQuery(id);
        return await _requestExecutor.Execute<GetLogErrorByIdQuery, Response<ItemLogErrorDto>>(query, token);
    }
}
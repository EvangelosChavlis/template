// source
using server.src.Domain.Dto.Common;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Models.Common;
using server.src.Application.Metrics.Errors.Interfaces;
using server.src.Application.Metrics.Errors.Queries;
using server.src.Domain.Dto.Metrics;

namespace server.src.Application.Metrics.Errors.Services;

public class ErrorQueries : IErrorQueries
{
    private readonly IRequestHandler<GetErrorsQuery, ListResponse<List<ListItemErrorDto>>> _getErrorsHandler;
    private readonly IRequestHandler<GetErrorByIdQuery, Response<ItemErrorDto>> _getErrorByIdHandler;

    public ErrorQueries(
        IRequestHandler<GetErrorsQuery, ListResponse<List<ListItemErrorDto>>> getErrorsHandler,
        IRequestHandler<GetErrorByIdQuery, Response<ItemErrorDto>> getErrorByIdHandler)
    {
        _getErrorsHandler = getErrorsHandler;
        _getErrorByIdHandler = getErrorByIdHandler;
    }

    public async Task<ListResponse<List<ListItemErrorDto>>> GetErrorsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetErrorsQuery(urlQuery);
        return await _getErrorsHandler.Handle(query, token);
    }

    public async Task<Response<ItemErrorDto>> GetErrorByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetErrorByIdQuery(id);
        return await _getErrorByIdHandler.Handle(query, token);
    }
}
// source
using server.src.Application.Weather.Warnings.Interfaces;
using server.src.Application.Weather.Warnings.Queries;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Application.Interfaces;
using server.src.Domain.Models.Common;

namespace server.src.Application.Weather.Warnings.Services;

public class WarningQueries : IWarningQueries
{
    private readonly IRequestHandler<GetWarningsQuery, ListResponse<List<ListItemWarningDto>>> _getWarningsHandler;
    private readonly IRequestHandler<GetWarningsPickerQuery, Response<List<PickerWarningDto>>> _getWarningsPickerHandler;
    private readonly IRequestHandler<GetWarningByIdQuery, Response<ItemWarningDto>> _getWarningByIdHandler;

    public WarningQueries(
        IRequestHandler<GetWarningsQuery, ListResponse<List<ListItemWarningDto>>> getWarningsHandler,
        IRequestHandler<GetWarningsPickerQuery, Response<List<PickerWarningDto>>> getWarningsPickerHandler,
        IRequestHandler<GetWarningByIdQuery, Response<ItemWarningDto>> getWarningByIdHandler)
    {
        _getWarningsHandler = getWarningsHandler;
        _getWarningsPickerHandler = getWarningsPickerHandler;
        _getWarningByIdHandler = getWarningByIdHandler;
    }

    public async Task<ListResponse<List<ListItemWarningDto>>> GetWarningsAsync(UrlQuery urlQuery, 
        CancellationToken token = default)
    {
        var query = new GetWarningsQuery(urlQuery);
        return await _getWarningsHandler.Handle(query, token);
    }

    public async Task<Response<List<PickerWarningDto>>> GetWarningsPickerAsync(CancellationToken token = default)
    {
        var query = new GetWarningsPickerQuery();
        return await _getWarningsPickerHandler.Handle(query, token);
    }

    public async Task<Response<ItemWarningDto>> GetWarningByIdAsync(Guid id, 
        CancellationToken token = default)
    {
        var query = new GetWarningByIdQuery(id);
        return await _getWarningByIdHandler.Handle(query, token);
    }
}
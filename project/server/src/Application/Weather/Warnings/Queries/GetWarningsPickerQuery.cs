using System.Net;
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Queries;

public record GetWarningsPickerQuery() : IRequest<Response<List<PickerWarningDto>>>;

public class GetWarningsPickerHandler : IRequestHandler<GetWarningsPickerQuery, Response<List<PickerWarningDto>>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public GetWarningsPickerHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerWarningDto>>> Handle(GetWarningsPickerQuery query, CancellationToken token = default)
    {
        var warnings = await _commonRepository.GetResultPickerAsync(_context.Warnings, token);
        var dto = warnings.Select(o => o.PickerWarningDtoMapping()).ToList();

        var success = dto.Count > 0;

        return new Response<List<PickerWarningDto>>()
            .WithMessage(success ? "Warnings fetched successfully." 
                : "No warnings found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}

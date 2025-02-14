// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Warnings.Mappings;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Queries;

public record GetWarningsPickerQuery() : IRequest<Response<List<PickerWarningDto>>>;

public class GetWarningsPickerHandler : IRequestHandler<GetWarningsPickerQuery, Response<List<PickerWarningDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetWarningsPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerWarningDto>>> Handle(GetWarningsPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<Warning, bool>>[] { };
        var warnings = await _commonRepository.GetResultPickerAsync(filters, token);

        // Mapping
        var dto = warnings.Select(o => o.PickerWarningDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerWarningDto>>()
            .WithMessage(success ? "Warnings fetched successfully." 
                : "No warnings found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}

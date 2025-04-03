// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Weather.Collections.MoonPhases.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Dtos;
using server.src.Domain.Weather.Collections.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.MoonPhases.Queries;

public record GetMoonPhasesPickerQuery() : IRequest<Response<List<PickerMoonPhaseDto>>>;

public class GetMoonPhasesPickerHandler : IRequestHandler<GetMoonPhasesPickerQuery, Response<List<PickerMoonPhaseDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetMoonPhasesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerMoonPhaseDto>>> Handle(GetMoonPhasesPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<MoonPhase, bool>>[] { w => w.IsActive };
        var moonphases = await _commonRepository.GetResultPickerAsync<MoonPhase, MoonPhase>(filters, token: token);

        // Mapping
        var dto = moonphases.Select(m => m.PickerMoonPhaseDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerMoonPhaseDto>>()
            .WithMessage(success ? "Moon phases fetched successfully." 
                : "No moo nphases found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}

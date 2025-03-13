// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Geography.Natural.Timezones.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Timezones.Queries;

public record GetTimezonesPickerQuery() : IRequest<Response<List<PickerTimezoneDto>>>;

public class GetTimezonesPickerHandler : IRequestHandler<GetTimezonesPickerQuery, Response<List<PickerTimezoneDto>>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTimezonesPickerHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<List<PickerTimezoneDto>>> Handle(GetTimezonesPickerQuery query, CancellationToken token = default)
    {
        // Searching Items
        var filters = new Expression<Func<Timezone, bool>>[] { t => t.IsActive };
        var timezones = await _commonRepository.GetResultPickerAsync<Timezone, Timezone>(filters, token: token);

        // Mapping
        var dto = timezones.Select(w => w.PickerTimezoneDtoMapping()).ToList();
        
        // Determine if the operation was successful
        var success = dto.Count > 0;

        // Initializing object
        return new Response<List<PickerTimezoneDto>>()
            .WithMessage(success ? "Timezones fetched successfully." 
                : "No timezones found matching the filter criteria.")
            .WithSuccess(success)
            .WithStatusCode(success ? (int)HttpStatusCode.OK 
                : (int)HttpStatusCode.NotFound)
            .WithData(dto);
    }
}

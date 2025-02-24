// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.Timezones.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Dtos;
using server.src.Domain.Geography.Natural.Timezones.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Timezones.Queries;

public record GetTimezoneByIdQuery(Guid Id) : IRequest<Response<ItemTimezoneDto>>;

public class GetTimezoneByIdHandler : IRequestHandler<GetTimezoneByIdQuery, Response<ItemTimezoneDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetTimezoneByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemTimezoneDto>> Handle(GetTimezoneByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemTimezoneDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemTimezoneDtoMapper
                    .ErrorItemTimezoneDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Timezone, bool>>[] { t => t.Id == query.Id };
        var timezone = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (timezone is null)
            return new Response<ItemTimezoneDto>()
                .WithMessage("Timezone not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemTimezoneDtoMapper
                    .ErrorItemTimezoneDtoMapping());

        // Mapping
        var dto = timezone.ItemTimezoneDtoMapping();

        // Initializing object
        return new Response<ItemTimezoneDto>()
            .WithMessage("Timezone fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

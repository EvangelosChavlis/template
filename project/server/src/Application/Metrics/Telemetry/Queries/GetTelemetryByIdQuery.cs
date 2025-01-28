// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Domain.Models.Metrics;
using server.src.Application.Interfaces;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Persistence.Interfaces;
using server.src.Application.Metrics.Telemetry.Mappings;

namespace server.src.Application.Metrics.Telemetry.Queries;

public record GetTelemetryByIdQuery(Guid Id) : IRequest<Response<ItemTelemetryDto>>;

public class GetTelemetryByIdHandler : IRequestHandler<GetTelemetryByIdQuery, Response<ItemTelemetryDto>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetTelemetryByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemTelemetryDto>> Handle(GetTelemetryByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<Telemetry, object>>[] { x => x.User };
        var filters = new Expression<Func<Telemetry, bool>>[] { x => x.Id == query.Id};
        var telemetry = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        // Check for existence
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
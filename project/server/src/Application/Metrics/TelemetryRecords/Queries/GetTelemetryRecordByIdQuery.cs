// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Dtos;
using server.src.Domain.Metrics.TelemetryRecords.Models;
using server.src.Application.Metrics.TelemetryRecords.Mappings;

namespace server.src.Application.Metrics.Telemetry.Queries;

public record GetTelemetryRecordByIdQuery(Guid Id) : IRequest<Response<ItemTelemetryRecordDto>>;

public class GetTelemetryRecordByIdHandler : IRequestHandler<GetTelemetryRecordByIdQuery, Response<ItemTelemetryRecordDto>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetTelemetryRecordByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemTelemetryRecordDto>> Handle(GetTelemetryRecordByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<TelemetryRecord, object>>[] { x => x.User };
        var filters = new Expression<Func<TelemetryRecord, bool>>[] { x => x.Id == query.Id};
        var telemetryRecord = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);

        // Check for existence
        if (telemetryRecord is null)
            return new Response<ItemTelemetryRecordDto>()
                .WithMessage("Telemetry record not found.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemTelemetryRecordDtoMapper
                    .ErrorItemTelemetryRecordDtoMapping());

        // Mapping
        var dto = telemetryRecord.ItemTelemetryRecordDtoMapping();

        // Initializing object
          return new Response<ItemTelemetryRecordDto>()
            .WithMessage("Telemetry record fetched successfully.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
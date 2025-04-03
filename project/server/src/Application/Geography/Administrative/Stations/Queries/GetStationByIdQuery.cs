// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Stations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Dtos;
using server.src.Domain.Geography.Administrative.Stations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Stations.Queries;

public record GetStationByIdQuery(Guid Id) : IRequest<Response<ItemStationDto>>;

public class GetStationByIdHandler : IRequestHandler<GetStationByIdQuery, Response<ItemStationDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetStationByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemStationDto>> Handle(GetStationByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemStationDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemStationDtoMapper
                    .ErrorItemStationDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Station, bool>>[] { s => s.Id == query.Id };
        var includes = new Expression<Func<Station, object>>[] { s => s.Location };
        var station = await _commonRepository.GetResultByIdAsync(
            filters, 
            includes: includes, 
            token: token
        );

        // Check for existence
        if (station is null)
            return new Response<ItemStationDto>()
                .WithMessage("Station not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemStationDtoMapper
                    .ErrorItemStationDtoMapping());

        // Mapping
        var dto = station.ItemStationDtoMapping();

        // Initializing object
        return new Response<ItemStationDto>()
            .WithMessage("Station fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

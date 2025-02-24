// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Natural.Locations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Natural.Locations.Dtos;
using server.src.Domain.Geography.Natural.Locations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Natural.Locations.Queries;

public record GetLocationByIdQuery(Guid Id) : IRequest<Response<ItemLocationDto>>;

public class GetLocationByIdHandler : IRequestHandler<GetLocationByIdQuery, Response<ItemLocationDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetLocationByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemLocationDto>> Handle(GetLocationByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemLocationDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemLocationDtoMapper
                    .ErrorItemLocationDtoMapping());
         
        // Searching Item
        var includes = new Expression<Func<Location, object>>[] 
        { 
            l => l.Latitude,
            l => l.Longitude,
            l => l.Timezone
        };
        var filters = new Expression<Func<Location, bool>>[] { l => l.Id == query.Id };
        var location = await _commonRepository.GetResultByIdAsync(filters, includes, token: token);

        // Check for existence
        if (location is null)
            return new Response<ItemLocationDto>()
                .WithMessage("Location not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemLocationDtoMapper
                    .ErrorItemLocationDtoMapping());

        // Mapping
        var dto = location.ItemLocationDtoMapping();

        // Initializing object
        return new Response<ItemLocationDto>()
            .WithMessage("Location fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

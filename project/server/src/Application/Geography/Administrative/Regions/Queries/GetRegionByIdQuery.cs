// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Regions.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Dtos;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Regions.Queries;

public record GetRegionByIdQuery(Guid Id) : IRequest<Response<ItemRegionDto>>;

public class GetRegionByIdHandler : IRequestHandler<GetRegionByIdQuery, Response<ItemRegionDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetRegionByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemRegionDto>> Handle(GetRegionByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemRegionDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemRegionDtoMapper
                    .ErrorItemRegionDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Region, bool>>[] { r => r.Id == query.Id };
        var region = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (region is null)
            return new Response<ItemRegionDto>()
                .WithMessage("Region not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemRegionDtoMapper
                    .ErrorItemRegionDtoMapping());

        // Mapping
        var dto = region.ItemRegionDtoMapping();

        // Initializing object
        return new Response<ItemRegionDto>()
            .WithMessage("Region fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

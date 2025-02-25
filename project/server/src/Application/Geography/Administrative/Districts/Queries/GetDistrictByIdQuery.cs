// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Districts.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Dtos;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Districts.Queries;

public record GetDistrictByIdQuery(Guid Id) : IRequest<Response<ItemDistrictDto>>;

public class GetDistrictByIdHandler : IRequestHandler<GetDistrictByIdQuery, Response<ItemDistrictDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetDistrictByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemDistrictDto>> Handle(GetDistrictByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemDistrictDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemDistrictDtoMapper
                    .ErrorItemDistrictDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<District, bool>>[] { r => r.Id == query.Id };
        var district = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (district is null)
            return new Response<ItemDistrictDto>()
                .WithMessage("District not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemDistrictDtoMapper
                    .ErrorItemDistrictDtoMapping());

        // Mapping
        var dto = district.ItemDistrictDtoMapping();

        // Initializing object
        return new Response<ItemDistrictDto>()
            .WithMessage("District fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

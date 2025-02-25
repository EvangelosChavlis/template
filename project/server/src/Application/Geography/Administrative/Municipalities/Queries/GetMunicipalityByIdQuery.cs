// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Municipalities.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Dtos;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Municipalities.Queries;

public record GetMunicipalityByIdQuery(Guid Id) : IRequest<Response<ItemMunicipalityDto>>;

public class GetMunicipalityByIdHandler : IRequestHandler<GetMunicipalityByIdQuery, Response<ItemMunicipalityDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetMunicipalityByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemMunicipalityDto>> Handle(GetMunicipalityByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemMunicipalityDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemMunicipalityDtoMapper
                    .ErrorItemMunicipalityDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Municipality, bool>>[] { r => r.Id == query.Id };
        var municipality = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (municipality is null)
            return new Response<ItemMunicipalityDto>()
                .WithMessage("Municipality not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemMunicipalityDtoMapper
                    .ErrorItemMunicipalityDtoMapping());

        // Mapping
        var dto = municipality.ItemMunicipalityDtoMapping();

        // Initializing object
        return new Response<ItemMunicipalityDto>()
            .WithMessage("Municipality fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

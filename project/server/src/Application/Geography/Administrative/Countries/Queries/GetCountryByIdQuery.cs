// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Countries.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Dtos;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Countries.Queries;

public record GetCountryByIdQuery(Guid Id) : IRequest<Response<ItemCountryDto>>;

public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, Response<ItemCountryDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetCountryByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemCountryDto>> Handle(GetCountryByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemCountryDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemCountryDtoMapper
                    .ErrorItemCountryDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Country, bool>>[] { c => c.Id == query.Id };
        var country = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (country is null)
            return new Response<ItemCountryDto>()
                .WithMessage("Country not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemCountryDtoMapper
                    .ErrorItemCountryDtoMapping());

        // Mapping
        var dto = country.ItemCountryDtoMapping();

        // Initializing object
        return new Response<ItemCountryDto>()
            .WithMessage("Country fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

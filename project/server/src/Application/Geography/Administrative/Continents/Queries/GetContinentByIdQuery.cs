// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Continents.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Continents.Queries;

public record GetContinentByIdQuery(Guid Id) : IRequest<Response<ItemContinentDto>>;

public class GetContinentByIdHandler : IRequestHandler<GetContinentByIdQuery, Response<ItemContinentDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetContinentByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemContinentDto>> Handle(GetContinentByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemContinentDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemContinentDtoMapper
                    .ErrorItemContinentDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Continent, bool>>[] { c => c.Id == query.Id };
        var continent = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (continent is null)
            return new Response<ItemContinentDto>()
                .WithMessage("Continent not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemContinentDtoMapper
                    .ErrorItemContinentDtoMapping());

        // Mapping
        var dto = continent.ItemContinentDtoMapping();

        // Initializing object
        return new Response<ItemContinentDto>()
            .WithMessage("Continent fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

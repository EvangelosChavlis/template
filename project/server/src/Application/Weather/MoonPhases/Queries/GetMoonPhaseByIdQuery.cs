// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.MoonPhases.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.MoonPhases.Dtos;
using server.src.Domain.Weather.MoonPhases.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.MoonPhases.Queries;

public record GetMoonPhaseByIdQuery(Guid Id) : IRequest<Response<ItemMoonPhaseDto>>;

public class GetMoonPhaseByIdHandler : IRequestHandler<GetMoonPhaseByIdQuery, Response<ItemMoonPhaseDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetMoonPhaseByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemMoonPhaseDto>> Handle(GetMoonPhaseByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemMoonPhaseDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemMoonPhaseDtoMapper
                    .ErrorItemMoonPhaseDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<MoonPhase, bool>>[] { m => m.Id == query.Id };
        var moonPhase = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (moonPhase is null)
            return new Response<ItemMoonPhaseDto>()
                .WithMessage("MoonPhase not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemMoonPhaseDtoMapper
                    .ErrorItemMoonPhaseDtoMapping());

        // Mapping
        var dto = moonPhase.ItemMoonPhaseDtoMapping();

        // Initializing object
        return new Response<ItemMoonPhaseDto>()
            .WithMessage("Moon phase fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

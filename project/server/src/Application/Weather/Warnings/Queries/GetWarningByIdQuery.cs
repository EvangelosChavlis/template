// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Interfaces;
using server.src.Application.Weather.Warnings.Mappings;
using server.src.Application.Weather.Warnings.Validators;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Queries;

public record GetWarningByIdQuery(Guid Id) : IRequest<Response<ItemWarningDto>>;

public class GetWarningByIdHandler : IRequestHandler<GetWarningByIdQuery, Response<ItemWarningDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetWarningByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemWarningDto>> Handle(GetWarningByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = WarningValidators.Validate(query.Id);
        if (!validationResult.IsValid)
            return new Response<ItemWarningDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(WarningsMappings.ErrorItemWarningDtoMapping());
         
        // Searching Item
        var includes = new Expression<Func<Warning, object>>[] { };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == query.Id };
        var warning = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        // Check for existence
        if (warning is null)
            return new Response<ItemWarningDto>()
                .WithMessage("Warning not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(WarningsMappings.ErrorItemWarningDtoMapping());

        // Mapping
        var dto = warning.ItemWarningDtoMapping();

        // Initializing object
        return new Response<ItemWarningDto>()
            .WithMessage("Warning fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

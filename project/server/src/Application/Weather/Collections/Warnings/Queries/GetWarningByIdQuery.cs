// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.Collections.Warnings.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Collections.Warnings.Dtos;
using server.src.Domain.Weather.Collections.Warnings.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Collections.Warnings.Queries;

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
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemWarningDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemWarningDtoMapper
                    .ErrorItemWarningDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Warning, bool>>[] { w => w.Id == query.Id };
        var warning = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (warning is null)
            return new Response<ItemWarningDto>()
                .WithMessage("Warning not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemWarningDtoMapper
                    .ErrorItemWarningDtoMapping());

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

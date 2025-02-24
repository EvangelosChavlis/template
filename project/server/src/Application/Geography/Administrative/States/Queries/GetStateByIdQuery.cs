// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.States.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.States.Dtos;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.States.Queries;

public record GetStateByIdQuery(Guid Id) : IRequest<Response<ItemStateDto>>;

public class GetStateByIdHandler : IRequestHandler<GetStateByIdQuery, Response<ItemStateDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetStateByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemStateDto>> Handle(GetStateByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemStateDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemStateDtoMapper
                    .ErrorItemStateDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<State, bool>>[] { c => c.Id == query.Id };
        var state = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (state is null)
            return new Response<ItemStateDto>()
                .WithMessage("State not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemStateDtoMapper
                    .ErrorItemStateDtoMapping());

        // Mapping
        var dto = state.ItemStateDtoMapping();

        // Initializing object
        return new Response<ItemStateDto>()
            .WithMessage("State fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

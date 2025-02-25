// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Geography.Administrative.Neighborhoods.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Dtos;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Geography.Administrative.Neighborhoods.Queries;

public record GetNeighborhoodByIdQuery(Guid Id) : IRequest<Response<ItemNeighborhoodDto>>;

public class GetNeighborhoodByIdHandler : IRequestHandler<GetNeighborhoodByIdQuery, Response<ItemNeighborhoodDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetNeighborhoodByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemNeighborhoodDto>> Handle(GetNeighborhoodByIdQuery query, CancellationToken token = default)
    {
        // Validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemNeighborhoodDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemNeighborhoodDtoMapper
                    .ErrorItemNeighborhoodDtoMapping());
         
        // Searching Item
        var filters = new Expression<Func<Neighborhood, bool>>[] { r => r.Id == query.Id };
        var neighborhood = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (neighborhood is null)
            return new Response<ItemNeighborhoodDto>()
                .WithMessage("Neighborhood not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemNeighborhoodDtoMapper
                    .ErrorItemNeighborhoodDtoMapping());

        // Mapping
        var dto = neighborhood.ItemNeighborhoodDtoMapping();

        // Initializing object
        return new Response<ItemNeighborhoodDto>()
            .WithMessage("Neighborhood fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

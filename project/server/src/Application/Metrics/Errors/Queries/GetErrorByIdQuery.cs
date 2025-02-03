// packagess
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Metrics.Errors.Mappings;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Metrics;
using server.src.Domain.Models.Errors;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Metrics.Errors.Queries;

public record GetErrorByIdQuery(Guid Id) : IRequest<Response<ItemErrorDto>>;

public class GetErrorByIdHandler : IRequestHandler<GetErrorByIdQuery, Response<ItemErrorDto>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetErrorByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemErrorDto>> Handle(GetErrorByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var includes = new Expression<Func<LogError, object>>[] { };
        var filters = new Expression<Func<LogError, bool>>[] { x => x.Id == query.Id};
        var warning = await _commonRepository.GetResultByIdAsync(filters, includes, token);

        if (warning is null)
            return new Response<ItemErrorDto>()
                .WithMessage("Error not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorsMappings.ItemErrorDtoMapping());

        // Mapping
        var dto = warning.ItemErrorDtoMapping();

        // Initializing object
        return new Response<ItemErrorDto>()
            .WithMessage("Error fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
   
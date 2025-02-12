// packagess
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Metrics.LogErrors.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Metrics.LogErrors.Dtos;
using server.src.Domain.Metrics.LogErrors.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Metrics.LogErrors.Queries;

public record GetLogErrorByIdQuery(Guid Id) : IRequest<Response<ItemLogErrorDto>>;

public class GetLogErrorByIdHandler : IRequestHandler<GetLogErrorByIdQuery, Response<ItemLogErrorDto>>
{
    private readonly ICommonRepository _commonRepository;
    
    public GetLogErrorByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemLogErrorDto>> Handle(GetLogErrorByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var filters = new Expression<Func<LogError, bool>>[] { x => x.Id == query.Id};
        var logError = await _commonRepository.GetResultByIdAsync(filters, token: token);

        if (logError is null)
            return new Response<ItemLogErrorDto>()
                .WithMessage("Error not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ItemErrorDtoMapper.ItemLogErrorDtoMapping());

        // Mapping
        var dto = logError.ItemLogErrorDtoMapping();

        // Initializing object
        return new Response<ItemLogErrorDto>()
            .WithMessage("Error fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
   
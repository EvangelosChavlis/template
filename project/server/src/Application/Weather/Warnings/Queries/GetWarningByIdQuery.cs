using System.Linq.Expressions;
using System.Net;
using server.src.Application.Interfaces;
using server.src.Application.Mappings.Weather;
using server.src.Domain.Dto.Common;
using server.src.Domain.Dto.Weather;
using server.src.Domain.Models.Weather;
using server.src.Persistence.Contexts;
using server.src.Persistence.Interfaces;

namespace server.src.Application.Weather.Warnings.Queries;

public record GetWarningByIdQuery(Guid Id) : IRequest<Response<ItemWarningDto>>;

public class GetWarningByIdHandler : IRequestHandler<GetWarningByIdQuery, Response<ItemWarningDto>>
{
    private readonly DataContext _context;
    private readonly ICommonRepository _commonRepository;

    public GetWarningByIdHandler(DataContext context, ICommonRepository commonRepository)
    {
        _context = context;
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemWarningDto>> Handle(GetWarningByIdQuery query, CancellationToken token = default)
    {
        var includes = new Expression<Func<Warning, object>>[] { };
        var filters = new Expression<Func<Warning, bool>>[] { x => x.Id == query.Id };
        var warning = await _commonRepository.GetResultByIdAsync(_context.Warnings, filters, includes, token);

        if (warning is null)
            return new Response<ItemWarningDto>()
                .WithMessage("Warning not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(WarningsMappings.ErrorItemWarningDtoMapping());

        var dto = warning.ItemWarningDtoMapping();

        return new Response<ItemWarningDto>()
            .WithMessage("Warning fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}

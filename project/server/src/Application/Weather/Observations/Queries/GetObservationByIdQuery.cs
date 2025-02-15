// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Common.Interfaces;
using server.src.Application.Common.Validators;
using server.src.Application.Weather.Observations.Mappings;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Weather.Observations.Dtos;
using server.src.Domain.Weather.Observations.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Weather.Observations.Queries;

public record GetObservationByIdQuery(Guid Id) : IRequest<Response<ItemObservationDto>>;

public class GetObservationByIdHandler : IRequestHandler<GetObservationByIdQuery, Response<ItemObservationDto>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetObservationByIdHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ItemObservationDto>> Handle(GetObservationByIdQuery query, CancellationToken token = default)
    {
        // validation
        var validationResult = query.Id.ValidateId();
        if (!validationResult.IsValid)
            return new Response<ItemObservationDto>()
                .WithMessage(string.Join("\n", validationResult.Errors))
                .WithSuccess(validationResult.IsValid)
                .WithData(ErrorItemObservationDtoMapper
                    .ErrorItemObservationDtoMapping());

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<Observation, bool>>[] { o => o.Id == query.Id};
        var observation = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (observation is null)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<ItemObservationDto>()
                .WithMessage("Observation not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemObservationDtoMapper
                    .ErrorItemObservationDtoMapping());
        }
            

        // Mapping
        var dto = observation.ItemObservationDtoMapping();
            
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<ItemObservationDto>()
            .WithMessage("Observation fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
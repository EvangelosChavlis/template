// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Roles.Mappings;
using server.src.Application.Auth.Roles.Validators;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Roles.Dtos;
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Common.Dtos;
using server.src.Persistence.Common.Contexts;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Roles.Commands;

public record CreateRoleCommand(CreateRoleDto Dto) : IRequest<Response<string>>;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Response<string>>
{
    private readonly ICommonRepository _commonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DataContext _context;
    
    public CreateRoleHandler(ICommonRepository commonRepository, IUnitOfWork unitOfWork, DataContext context)
    {
        _commonRepository = commonRepository;
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<Response<string>> Handle(CreateRoleCommand command, CancellationToken token = default)
    {
        // Dto Validation
        var dtoValidationResult = command.Dto.Validate();
        if (!dtoValidationResult.IsValid)
            return new Response<string>()
                .WithMessage("Dto validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(dtoValidationResult.IsValid)
                .WithData(string.Join("\n", dtoValidationResult.Errors));

        // Begin Transaction
        await _unitOfWork.BeginTransactionAsync(token);

        // Searching Item
        var filters = new Expression<Func<Role, bool>>[] { r => r.Name!.Equals(command.Dto.Name)};
        var existingRole = await _commonRepository.AnyExistsAsync(filters, token);
        
        // Check if the role already exists in the system
        if(existingRole)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating role.")
                .WithStatusCode((int)HttpStatusCode.Conflict)
                .WithSuccess(false)
                .WithData($"Role with name {command.Dto.Name} already exists.");
        }

        // Mapping, Validating, Saving Item
        var role = command.Dto.CreateRoleModelMapping();
        var modelValidationResult = role.Validate();
        if (!modelValidationResult.IsValid)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Entity validation failed.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(modelValidationResult.IsValid)
                .WithData(string.Join("\n", modelValidationResult.Errors));
        }
        var result = await _commonRepository.AddAsync(role, token);
        
        // Saving failed
        if(!result)
        {
            await _unitOfWork.RollbackTransactionAsync(token);
            return new Response<string>()
                .WithMessage("Error creating role.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(result)
                .WithData("Failed to create role.");
        }
        
        // Commit Transaction
        await _unitOfWork.CommitTransactionAsync(token);

        // Initializing object
        return new Response<string>()
            .WithMessage("Success creating role.")
            .WithStatusCode((int)HttpStatusCode.Created)
            .WithSuccess(result)
            .WithData($"Role {role.Name} created successfully.");
    }
}
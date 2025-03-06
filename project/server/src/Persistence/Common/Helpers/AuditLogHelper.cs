// packages
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

// source
using server.src.Persistence.Common.Contexts;
using server.src.Persistence.Common.Interfaces;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Metrics.AuditLogs.Enums;
using server.src.Domain.Metrics.AuditLogs.Models;
using server.src.Domain.Metrics.Trails;
using System.Linq;

namespace server.src.Persistence.Common.Helpers;

public class AuditLogHelper : IAuditLogHelper
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;
    
    public AuditLogHelper(DataContext dataContext, IHttpContextAccessor httpContextAccessor, 
        IUnitOfWork unitOfWork)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<string>> CreateAuditLogAsync<TEntity>(TEntity oldEntityData, TEntity newEntityData, 
        CancellationToken token = default)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var userName = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;

        var filters = new Expression<Func<User, bool>>[] { u => u.UserName == userName };
        var user = await _dataContext.AuthDbSets.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync(token);

        if (user is null)
            return new Response<string>()
                .WithMessage("Error creating audit log.")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData($"User with username '{userName}' not found.");

        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";

        var entityType = typeof(TEntity).Name;
        var idProperty = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

        if (idProperty is null)
            return new Response<string>()
                .WithMessage("Error creating audit log.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Entity '{entityType}' does not contain an 'Id' property.");

        var idValue = idProperty.GetValue(newEntityData ?? oldEntityData);
        if (idValue is not Guid entityId)
            return new Response<string>()
                .WithMessage("Error creating audit log.")
                .WithStatusCode((int)HttpStatusCode.BadRequest)
                .WithSuccess(false)
                .WithData($"Invalid 'Id' property in entity '{entityType}'. Expected a valid Guid.");

        var metadata = newEntityData is not null ? 
            JsonSerializer.Serialize(newEntityData) : null;
        var beforeValues = oldEntityData is not null ? 
            JsonSerializer.Serialize(oldEntityData) : null;
        var afterValues = newEntityData is not null ? 
            JsonSerializer.Serialize(newEntityData) : null;

        var methodName = new StackTrace().GetFrame(1)?.GetMethod()?.Name ?? "UnknownService";

        ActionType actionType;
        if (oldEntityData is null)
            actionType = ActionType.Created;
        else if (newEntityData is null)
            actionType = ActionType.Deleted;
        else
            actionType = ActionType.Updated;

        var auditLog = new AuditLog
        {
            EntityId = entityId,
            EntityType = entityType,
            Action = actionType,
            Timestamp = DateTime.UtcNow,
            IPAddress = ipAddress,
            Reason = metadata,
            AdditionalMetadata = metadata,
            BeforeValues = beforeValues,
            AfterValues = afterValues,
            UserId = user.Id,
            User = user,
        };

        await _dataContext.MetricsDbSets.AuditLogs.AddAsync(auditLog, token);
        var result = await _unitOfWork.CommitAsync(token);

        if (!result)
            return new Response<string>()
                .WithMessage("Error creating audit log.")
                .WithStatusCode((int)HttpStatusCode.InternalServerError)
                .WithSuccess(false)
                .WithData("An error occurred while creating the audit log. Please try again.");

        // Create trail relationships if needed
        if (actionType != ActionType.Created)
        {
            // Get the last audit log entry for the entity
            var previousAuditLog = await GetLastAuditLogAsync(entityId, entityType, token);
            if (previousAuditLog != null)
            {
                var trail = TrailMapping(auditLog, previousAuditLog);

                await _dataContext.MetricsDbSets.Trails.AddAsync(trail, token);
                var chainResult = await _unitOfWork.CommitAsync(token);

                if (!chainResult)
                    return new Response<string>()
                        .WithMessage("Error creating chain relationship.")
                        .WithStatusCode((int)HttpStatusCode.InternalServerError)
                        .WithSuccess(false)
                        .WithData("An error occurred while creating the chain relationship. Please try again.");
            }
        }

        return new Response<string>()
            .WithMessage("Successfully created audit log.")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData("Audit log created successfully.");
    }

    private static Trail TrailMapping(AuditLog auditLog, AuditLog previousAuditLog)
    {
        return new Trail
        {
            SourceAuditLogId = previousAuditLog.Id,
            TargetAuditLogId = auditLog.Id,
            Timestamp = DateTime.UtcNow
        };
    }

    private async Task<AuditLog?> GetLastAuditLogAsync(Guid entityId, string entityType, CancellationToken token)
    {
        return await _dataContext.MetricsDbSets.AuditLogs
            .Where(log => log.EntityId == entityId && log.EntityType == entityType)
            .OrderByDescending(log => log.Timestamp)
            .FirstOrDefaultAsync(token);
    }
}

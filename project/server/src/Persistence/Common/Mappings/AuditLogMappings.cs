// source
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Metrics.AuditLogs.Enums;
using server.src.Domain.Metrics.AuditLogs.Models;

namespace server.src.Persistence.Common.Mappings;

public static class AuditLogMappings
{
    public static AuditLog CreateAuditLogModelMapping(
        this  User userModel,
        Guid id,
        string entityType,
        ActionType actionType, 
        string reason, 
        string metadata, 
        string IPAddress)
    {
        return new AuditLog
        {
            EntityId = id,
            EntityType = entityType,
            Action = actionType,
            Timestamp = DateTime.UtcNow,
            IPAddress = IPAddress,
            Reason = reason,
            AdditionalMetadata = metadata,
            UserId = userModel.Id,
            User = userModel,
        };
    }


    public static AuditLog UpdateAuditLogModelMapping(
        this  User userModel,
        Guid id, 
        string entityType, 
        string reason, 
        string metadata, 
        string IPAddress)
    {
        return new AuditLog
        {
            EntityId = id,
            EntityType = entityType,
            Action = ActionType.Created,
            Timestamp = DateTime.UtcNow,
            IPAddress = IPAddress,
            Reason = reason,
            AdditionalMetadata = metadata,
            UserId = userModel.Id,
            User = userModel,
        };
    }
}
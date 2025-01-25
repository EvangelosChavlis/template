// source
using server.src.Domain.Models.Auth;
using server.src.Domain.Models.Enums;
using server.src.Domain.Models.Metrics;

namespace server.src.Persistence.Mappings;

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
// source
using server.src.Domain.Dto.Common;

namespace server.src.Persistence.Helpers;

public interface IAuditLogHelper
{
    Task<Response<string>> CreateAuditLogAsync<TEntity>(TEntity oldEntityData, TEntity newEntityData, 
        CancellationToken token = default);
}
// source
using server.src.Domain.Common.Dtos;

namespace server.src.Persistence.Common.Helpers;

public interface IAuditLogHelper
{
    Task<Response<string>> CreateAuditLogAsync<TEntity>(TEntity oldEntityData, TEntity newEntityData, 
        CancellationToken token = default);
}
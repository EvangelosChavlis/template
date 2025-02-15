// source
using server.src.Domain.Auth.Roles.Models;
using server.src.Domain.Common.Models;
using server.src.Domain.Developer.Aggreggates.Models;

namespace server.src.Domain.Auth.Permissions.Models;

public class Permission : BaseEntity
{
    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; }
    public virtual List<Aggregate> Aggregates { get; set; }
}
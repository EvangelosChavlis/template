// source
using server.src.Domain.Common.Models;

namespace server.src.Domain.Auth.Permissions.Models;

public class Permission : BaseEntity
{
    public string Action { get; set; }
}
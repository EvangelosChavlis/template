// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Mappings.Auth;

public static class RolesMappings
{

    public static ItemRoleDto ItemRoleDtoMapping(this Role model)
        => new (
            Id: model.Id,

            Name: model.Name!,
            Description: model.Description,

            IsActive: model.IsActive
        );

    public static Role CreateRoleModelMapping(this RoleDto dto)
        => new()
        {
            Name = dto.Name,
            Description = dto.Description,
            
            IsActive = true
        };

    public static void UpdateRoleModelMapping(this RoleDto dto, Role model)
    {
        model.Name = dto.Name;
        model.Description = dto.Description;
        
        model.IsActive = model.IsActive;
    }
}
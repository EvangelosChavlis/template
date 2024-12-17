// packages
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// source
using server.src.Application.Interfaces.Auth;
using server.src.Application.Mappings.Auth;
using server.src.Domain.Dto.Auth;
using server.src.Domain.Dto.Common;
using server.src.Domain.Exceptions;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Services.Auth;

public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<List<ItemRoleDto>> GetRolesService()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        var dto = roles.Select(r => r.ItemRoleDtoMapping()).ToList();
        return dto;
    }

    public async Task<ItemResponse<ItemRoleDto>> GetRoleByIdService(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? 
            throw new CustomException("Role not found.", (int)HttpStatusCode.NotFound);

        var dto = role.ItemRoleDtoMapping();
        return new ItemResponse<ItemRoleDto>().WithData(dto);
    }

    public async Task<CommandResponse<string>> CreateRoleService(RoleDto dto)
    {
        var existingRole = await _roleManager.FindByNameAsync(dto.Name);
        if(existingRole != null)
            throw new CustomException($"Role with name {existingRole.Name} already exists");

        var role = dto.CreateRoleModelMapping();
        var result = await _roleManager.CreateAsync(role);

        if(!result.Succeeded)
            throw new CustomException("Failed to create role.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Role {role.Name} created successfully");
    }

    public async Task<CommandResponse<string>> InitializeRolesService(List<RoleDto> dto)
    {
        var success = false;
        var messageBuilder = new StringBuilder();
        foreach (var item in dto)
        {
            var result = await CreateRoleService(item);
            success = result.Success;

            messageBuilder.AppendLine(result.Data);
        }

        var message = messageBuilder.ToString().TrimEnd();


        return new CommandResponse<string>()
            .WithSuccess(success)
            .WithData(message);
    }


    public async Task<CommandResponse<string>> UpdateRoleService(string id, RoleDto dto)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? 
            throw new CustomException("Role not found.", (int)HttpStatusCode.NotFound);

        dto.UpdateRoleModelMapping(role);

        var result = await _roleManager.UpdateAsync(role);

        if(!result.Succeeded)
            throw new CustomException("Failed to update role.", (int)HttpStatusCode.BadRequest);

        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Role {role.Name} updated successfully");
    }


    public async Task<CommandResponse<string>> ActivateRoleService(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? 
            throw new CustomException("Role not found.", (int)HttpStatusCode.NotFound);

        if (role.IsActive)
            throw new CustomException("Role is activated.", (int)HttpStatusCode.BadRequest);

        role.IsActive = true;

        var result = await _roleManager.UpdateAsync(role);

        if(!result.Succeeded)
            throw new CustomException("Failed to activate user.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Role {role.Name} activated successfully");
    }

    public async Task<CommandResponse<string>> DeactivateRoleService(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? 
            throw new CustomException("Role not found.", (int)HttpStatusCode.NotFound);

        if (!role.IsActive)
            throw new CustomException("Role is deactivated.", (int)HttpStatusCode.BadRequest);

        role.IsActive = false;

        var result = await _roleManager.UpdateAsync(role);

        if(!result.Succeeded)
            throw new CustomException("Failed to deactivate role.", (int)HttpStatusCode.BadRequest);
    
        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Role {role.Name} deactivated successfully");
    }


    public async Task<CommandResponse<string>> DeleteRoleService(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? 
            throw new CustomException("Role not found.", (int)HttpStatusCode.NotFound);
        
        var result =  await _roleManager.DeleteAsync(role);

        if(!result.Succeeded)
            throw new CustomException("Failed to delete role.", (int)HttpStatusCode.BadRequest);

        return new CommandResponse<string>()
            .WithSuccess(result.Succeeded)
            .WithData($"Role {role.Name} deleted successfully");
    }
}

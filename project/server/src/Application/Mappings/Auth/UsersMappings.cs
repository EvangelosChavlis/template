// source
using server.src.Domain.Dto.Auth;
using server.src.Domain.Extensions;
using server.src.Domain.Models.Auth;

namespace server.src.Application.Mappings.Auth;

public static class UsersMappings
{
    public static ListItemUserDto ListItemUserDtoMapping(this User model)
        => new (
            Id: model.Id,
            FirstName: model.FirstName,
            LastName: model.LastName,
            Email: model.Email!,
            UserName: model.UserName!,
            PhoneNumber: model.PhoneNumber!,
            MobilePhoneNumber: model.MobilePhoneNumber
        );

    public static ItemUserDto ItemUserDtoMapping(this User model)
        => new (
            Id: model.Id,

            FirstName: model.FirstName,
            LastName: model.LastName,
            Email: model.Email!,
            EmailConfirmed: model.EmailConfirmed,
            UserName: model.UserName!,
            LockoutEnabled: model.LockoutEnabled,
            LockoutEnd: model.LockoutEnd.ToString()!,
            InitialPassword: model.InitialPassword,
            
            Address: model.Address,
            ZipCode: model.ZipCode,
            City: model.City,
            State: model.State,
            Country: model.Country,
            PhoneNumber: model.PhoneNumber!,
            PhoneNumberConfirmed: model.PhoneNumberConfirmed,
            MobilePhoneNumber: model.MobilePhoneNumber,
            MobilePhoneNumberConfirmed: model.MobilePhoneNumberConfirmed,

            Bio: model.Bio,
            DateOfBirth: model.DateOfBirth.GetLocalDateTimeString(),

            IsActive: model.IsActive,

            Roles: model.UserRoles.Select(ur => ur.Role.Name).ToList()!
        );

    public static User CreateUserModelMapping(this UserDto dto, bool registered)
        => new()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.UserName,
            InitialPassword = registered ? "from registration" : dto.Password,
            EmailConfirmed = false,
            LockoutEnabled = false,

            Address = dto.Address,
            ZipCode = dto.ZipCode,
            City = dto.City,
            State = dto.State,
            Country = dto.Country,
            PhoneNumber = dto.PhoneNumber,
            PhoneNumberConfirmed = false,
            MobilePhoneNumber = dto.MobilePhoneNumber,
            MobilePhoneNumberConfirmed = false,
            
            Bio = dto.Bio,
            DateOfBirth = dto.DateOfBirth,

            IsActive = true
        };

    public static void UpdateUserModelMapping(this UserDto dto, User model)
    {
        model.FirstName = dto.FirstName;
        model.LastName = dto.LastName;
        model.Email = dto.Email;
        model.UserName = dto.UserName;
        model.EmailConfirmed = model.EmailConfirmed;

        model.Address = dto.Address;
        model.ZipCode = dto.ZipCode;
        model.City = dto.City;
        model.State = dto.State;
        model.Country = dto.Country;
        model.PhoneNumber = dto.PhoneNumber;
        model.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
        model.MobilePhoneNumber = dto.MobilePhoneNumber;
        model.MobilePhoneNumberConfirmed = model.MobilePhoneNumberConfirmed;
        
        model.Bio = dto.Bio;
        model.DateOfBirth = dto.DateOfBirth;
    }

    public static AuthenticatedUserDto AuthenticatedUserDtoMapping(this string userName, string token) 
        => new (UserName: userName, Token: token);
}
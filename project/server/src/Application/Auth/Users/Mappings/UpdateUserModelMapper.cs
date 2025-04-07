// source
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;

namespace server.src.Application.Auth.Users.Mappings;

/// <summary>
/// Provides a method to update an existing <see cref="User"/> model using data 
/// from an <see cref="UpdateUserDto"/>.
/// </summary>
public static class UpdateUserModelMapper
{
    /// <summary>
    /// Updates an existing User model with data from a UserDto.
    /// </summary>
    /// <param name="dto">The UserDto containing updated data.</param>
    /// <param name="model">The User model to be updated.</param>
    public static void UpdateUserModelMapping(this UpdateUserDto dto, 
        User model, Neighborhood neighborhoodModel)
    {
        model.FirstName = dto.FirstName;
        model.LastName = dto.LastName;
        model.Email = dto.Email;
        model.UserName = dto.UserName;
        model.EmailConfirmed = model.EmailConfirmed;

        model.Address = dto.Address;
        model.PhoneNumber = dto.PhoneNumber;
        model.PhoneNumberConfirmed = model.PhoneNumberConfirmed;
        model.MobilePhoneNumber = dto.MobilePhoneNumber;
        model.MobilePhoneNumberConfirmed = model.MobilePhoneNumberConfirmed;
        model.Neighborhood = neighborhoodModel;
        model.NeighborhoodId = neighborhoodModel.Id;
        
        model.Bio = dto.Bio;
        model.DateOfBirth = dto.DateOfBirth.ToString();
    }
}
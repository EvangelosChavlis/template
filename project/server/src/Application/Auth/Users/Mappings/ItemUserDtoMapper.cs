// source
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;

namespace server.src.Application.Auth.Users.Mappings;

/// <summary>
/// Provides extension methods for mapping <see cref="User"/> models to DTOs.
/// </summary>
public static class ItemUserDtoMapper
{
    /// <summary>
    /// Maps a <see cref="User"/> model to an <see cref="ItemUserDto"/>.
    /// </summary>
    /// <param name="model">The <see cref="User"/> model that will be mapped.</param>
    /// <returns>An <see cref="ItemUserDto"/> representing the <see cref="User"/> model.</returns>
    public static ItemUserDto ItemUserDtoMapping(
        this User model, 
        Neighborhood neighborhoodModel,
        District districtModel,
        Municipality municipalityModel,
        State stateModel,
        Region regionRegion,
        Country countryModel,
        Continent continentModel)
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
            
            Neighborhood: neighborhoodModel.Name,
            ZipCode: neighborhoodModel.Zipcode,
            Address: model.Address,
            District: districtModel.Name,
            Municipality: municipalityModel.Name,
            State: stateModel.Name,
            Region: regionRegion.Name,
            Country: countryModel.Name,
            Continent: continentModel.Name,
      
            PhoneNumber: model.PhoneNumber!,
            PhoneNumberConfirmed: model.PhoneNumberConfirmed,
            MobilePhoneNumber: model.MobilePhoneNumber,
            MobilePhoneNumberConfirmed: model.MobilePhoneNumberConfirmed,
            Bio: model.Bio,
            DateOfBirth: model.DateOfBirth,

            IsActive: model.IsActive,
            Version: Guid.Empty
        );
}

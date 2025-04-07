// packages
using System.Linq.Expressions;
using System.Net;

// source
using server.src.Application.Auth.Users.Mappings;
using server.src.Application.Common.Interfaces;
using server.src.Domain.Auth.Users.Dtos;
using server.src.Domain.Auth.Users.Models;
using server.src.Domain.Common.Dtos;
using server.src.Domain.Geography.Administrative.Continents.Models;
using server.src.Domain.Geography.Administrative.Countries.Models;
using server.src.Domain.Geography.Administrative.Districts.Models;
using server.src.Domain.Geography.Administrative.Municipalities.Models;
using server.src.Domain.Geography.Administrative.Neighborhoods.Models;
using server.src.Domain.Geography.Administrative.Regions.Models;
using server.src.Domain.Geography.Administrative.States.Models;
using server.src.Persistence.Common.Interfaces;

namespace server.src.Application.Auth.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<Response<ItemUserDto>>;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Response<ItemUserDto>>
{
    private readonly ICommonRepository _commonRepository;

    public GetUserByIdHandler(ICommonRepository commonRepository)
    {
        _commonRepository = commonRepository;
    }

    public async Task<Response<ItemUserDto>> Handle(GetUserByIdQuery query, CancellationToken token = default)
    {
        // Searching Item
        var filters = new Expression<Func<User, bool>>[] { u => u.Id == query.Id};
        var user = await _commonRepository.GetResultByIdAsync(filters, token: token);

        // Check for existence
        if (user is null)
            return new Response<ItemUserDto>()
                .WithMessage("User not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var neighborhoodfilters = new Expression<Func<Neighborhood, bool>>[] { n => n.Id == user.NeighborhoodId };
        var neighborhood = await _commonRepository.GetResultByIdAsync(neighborhoodfilters, token: token);

        // Check for existence
        if (neighborhood is null)
            return new Response<ItemUserDto>()
                .WithMessage("Neighborhood not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var districtfilters = new Expression<Func<District, bool>>[] { d => d.Id == neighborhood.DistrictId };
        var district = await _commonRepository.GetResultByIdAsync(districtfilters, token: token);

        // Check for existence
        if (district is null)
            return new Response<ItemUserDto>()
                .WithMessage("District not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var municipalityfilters = new Expression<Func<Municipality, bool>>[] { m => m.Id == district.MunicipalityId };
        var municipality = await _commonRepository.GetResultByIdAsync(municipalityfilters, token: token);

        // Check for existence
        if (municipality is null)
            return new Response<ItemUserDto>()
                .WithMessage("Municipality not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var regionfilters = new Expression<Func<Region, bool>>[] { r => r.Id == municipality.RegionId };
        var region = await _commonRepository.GetResultByIdAsync(regionfilters, token: token);

        // Check for existence
        if (region is null)
            return new Response<ItemUserDto>()
                .WithMessage("Region not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var statefilters = new Expression<Func<State, bool>>[] { s => s.Id == region.StateId };
        var state = await _commonRepository.GetResultByIdAsync(statefilters, token: token);

        // Check for existence
        if (state is null)
            return new Response<ItemUserDto>()
                .WithMessage("State not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var countryfilters = new Expression<Func<Country, bool>>[] { c => c.Id == state.CountryId };
        var country = await _commonRepository.GetResultByIdAsync(countryfilters, token: token);

        // Check for existence
        if (country is null)
            return new Response<ItemUserDto>()
                .WithMessage("Country not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Searching Item
        var continentfilters = new Expression<Func<Continent, bool>>[] { c => c.Id == state.CountryId };
        var continent = await _commonRepository.GetResultByIdAsync(continentfilters, token: token);

        // Check for existence
        if (continent is null)
            return new Response<ItemUserDto>()
                .WithMessage("Continent not found")
                .WithStatusCode((int)HttpStatusCode.NotFound)
                .WithSuccess(false)
                .WithData(ErrorItemUserDtoMapper.ErrorItemUserDtoMapping());

        // Mapping
        var dto = user.ItemUserDtoMapping(
            neighborhoodModel: neighborhood, 
            districtModel: district, 
            municipalityModel: municipality,
            stateModel: state,  
            regionRegion: region, 
            countryModel: country, 
            continentModel: continent
        );

        // Initializing object
        return new Response<ItemUserDto>()
            .WithMessage("User fetched successfully")
            .WithStatusCode((int)HttpStatusCode.OK)
            .WithSuccess(true)
            .WithData(dto);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases.Locations.DTO;
using InfectedBackEnd.Application.UseCases.Locations.Utils;
using InfectedBackEnd.Application.UseCases.User;
using InfectedBackEnd.Domain;
using InfectedBackEnd.Infrastructure.Database;

namespace InfectedBackEnd.Application.UseCases.Locations
{
    public interface ILocationsUseCase
    {
        Task<List<Location>> GetAllLocations(Guid token);
        Task<CreateLocationResponseDto> CreateLocation(CreateLocationRequestDto requestDto, Guid token);
    }

    public class LocationsUseCase : ILocationsUseCase
    {
        private readonly ILocationRepository locationRepository;
        private readonly IUserUseCase userUseCase;

        public LocationsUseCase(ILocationRepository locationRepository, IUserUseCase userUseCase)
        {
            this.locationRepository = locationRepository;
            this.userUseCase = userUseCase;
        }

        public async Task<List<Location>> GetAllLocations(Guid token)
        {
            var user = await userUseCase.GetUserByToken(token);
            if (user is null) return null;

            var locations = await locationRepository.GetAllLocations();

            return locations;
        }

        public async Task<CreateLocationResponseDto> CreateLocation(CreateLocationRequestDto requestDto, Guid token)
        {
            var user = await userUseCase.GetUserByToken(token);
            if (user is null) return null;

            var location = requestDto.ToLocationDomain();
            var _ = await locationRepository.CreateLocation(location);
            return location.ToCreateResponse();
        }
    }
}
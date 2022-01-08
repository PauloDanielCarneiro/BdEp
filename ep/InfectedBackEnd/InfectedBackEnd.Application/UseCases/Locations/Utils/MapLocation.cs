using System;
using InfectedBackEnd.Application.UseCases.Locations.DTO;

namespace InfectedBackEnd.Application.UseCases.Locations.Utils
{
    public static class MapLocation
    {
        public static CreateLocationResponseDto ToCreateResponse(this Domain.Location location)
        {
            return new(location.Id, location.Latitude, location.Longitude);
        }

        public static Domain.Location ToLocationDomain(this CreateLocationRequestDto locationDto)
        {
            return new(Guid.NewGuid(), locationDto.Lat, locationDto.Long);
        }
    }
}
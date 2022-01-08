using System;
using InfectedBackEnd.Domain;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Response
{
    public class AddUserLocationResponseDto
    {
        public AddUserLocationResponseDto()
        {
            UserLocationId = Guid.Empty;
            Latitude = double.MinValue;
            Longitude = double.MinValue;
        }

        public AddUserLocationResponseDto(Guid userLocationId, double latitude, double longitude)
        {
            UserLocationId = userLocationId;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Guid UserLocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
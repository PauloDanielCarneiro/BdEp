using System;

namespace InfectedBackEnd.Application.UseCases.Locations.DTO
{
    public class CreateLocationResponseDto
    {
        public CreateLocationResponseDto(Guid id, double lat, double lg)
        {
            Id = id;
            Lat = lat;
            Long = lg;
        }

        public Guid Id { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
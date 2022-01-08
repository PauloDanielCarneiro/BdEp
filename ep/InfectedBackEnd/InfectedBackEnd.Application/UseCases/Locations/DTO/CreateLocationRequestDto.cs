using System;

namespace InfectedBackEnd.Application.UseCases.Locations.DTO
{
    public class CreateLocationRequestDto
    {
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}
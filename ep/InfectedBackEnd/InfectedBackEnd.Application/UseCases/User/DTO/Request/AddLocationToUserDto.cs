using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Request
{
    public class AddLocationToUserDto
    {
        public double Lat { get; set; }
        public double Long { get; set; }
        public DateTime Datetime { get; set; }
    }
}
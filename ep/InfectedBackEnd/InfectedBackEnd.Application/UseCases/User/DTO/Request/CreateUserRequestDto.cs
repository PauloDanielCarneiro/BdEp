using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Request
{
    public class CreateUserRequestDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
    }
}
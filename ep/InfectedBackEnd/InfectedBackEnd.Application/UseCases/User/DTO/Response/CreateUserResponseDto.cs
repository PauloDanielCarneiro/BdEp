using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Response
{
    public class CreateUserResponseDto
    {
        public CreateUserResponseDto(Guid id, string name, string email, Guid token)
        {
            Id = id;
            Name = name;
            Email = email;
            Token = token;
        }

        public CreateUserResponseDto()
        {
            Id = Guid.Empty;
            Name = "";
            Email = "";
            Token = Guid.Empty;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid Token { get; set; }
    }
}
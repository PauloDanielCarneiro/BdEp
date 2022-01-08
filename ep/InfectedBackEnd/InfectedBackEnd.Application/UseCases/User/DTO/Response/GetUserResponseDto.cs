using System;

namespace InfectedBackEnd.Application.UseCases.User.Dto
{
    public class GetUserResponseDto
    {
        public GetUserResponseDto()
        {
        }

        public GetUserResponseDto(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
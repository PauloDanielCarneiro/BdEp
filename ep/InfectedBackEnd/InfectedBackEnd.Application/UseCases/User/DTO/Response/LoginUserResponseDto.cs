using System;

namespace InfectedBackEnd.Application.UseCases.User.DTO.Response
{
    public class LoginUserResponseDto
    {
        public LoginUserResponseDto()
        {
            Document = string.Empty;
            Token = Guid.Empty;
        }

        public LoginUserResponseDto(string document, Guid token)
        {
            Document = document;
            Token = token;
        }

        public string Document { get; set; }
        public Guid Token { get; set; }
    }
}
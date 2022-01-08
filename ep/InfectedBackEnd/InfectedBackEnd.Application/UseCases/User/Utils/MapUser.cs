using InfectedBackEnd.Application.UseCases.User.DTO.Request;
using InfectedBackEnd.Application.UseCases.User.DTO.Response;

namespace InfectedBackEnd.Application.UseCases.User.Utils
{
    public static class MapUser
    {
        public static Domain.User ToUserDomain(this CreateUserRequestDto userRequestDto)
        {
            return new(userRequestDto.Name, userRequestDto.Password, userRequestDto.Email, userRequestDto.Document);
        }

        public static CreateUserResponseDto ToCreateResponse(this Domain.User user)
        {
            return new(user.Id, user.Name, user.Email, user.Token);
        }

        public static LoginUserResponseDto ToLoginResponse(this Domain.User user)
        {
            return new(user.Document, user.Token);
        }
    }
}
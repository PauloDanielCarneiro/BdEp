using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfectedBackEnd.Application.UseCases.User;
using InfectedBackEnd.Application.UseCases.User.DTO.Request;

namespace InfectedBackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public UserController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpGet]
        [Route("{document}")]
        public async Task<IActionResult> GetUser([FromRoute] string document, [FromHeader] Guid token)
        {
            var userDto = await _userUseCase.GetUser(document, token);
            return userDto switch
            {
                null => Unauthorized(),
                {Email: "", Name: ""} => NotFound(new {message = "User with this document not found!"}),
                _ => Ok(userDto)
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            var user = await _userUseCase.CreateUser(request);
            return user switch
            {
                {Email: "", Name: ""} => BadRequest(new
                    {message = "This email or document is either invalid or already used"}),
                _ => Ok(user)
            };
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto loginRequest)
        {
            var result = await _userUseCase.LoginUser(loginRequest);
            if (result.Token == Guid.Empty)
                return NotFound(new {message = "User with this document and password not found!"});
            return Ok(result);
        }

        [HttpPost]
        [Route("{userId}/locations")]
        public async Task<IActionResult> AddLocationToUser([FromRoute] Guid userId,
            [FromBody] AddLocationToUserDto locationToUser, [FromHeader] Guid token)
        {
            var result = await _userUseCase.AddLocationToUser(locationToUser, token, userId);
            return result switch
            {
                null => Unauthorized(),
                {Latitude: double.MinValue, Longitude: double.MinValue} => BadRequest(new
                    {message = "Could not register the Location to this user, revise your data and try again"}),
                _ => Ok(result)
            };
        }

        [HttpPost]
        [Route("{userId}/diseases")]
        public async Task<IActionResult> AddDiseaseToUser([FromBody] AddDiseaseToUserDto diseaseToUser,
            [FromHeader] Guid token, [FromRoute] Guid userId)
        {
            try
            {
                var result = await _userUseCase.AddDiseaseToUser(diseaseToUser, token, userId);
                return result switch
                {
                    null => Unauthorized(),
                    {Cured: null} => BadRequest(new
                        {message = "Could not register the Disease to this user, revise your data and try again"}),
                    _ => Ok(result)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpGet]
        [Route("{userId}/diseases")]
        public async Task<IActionResult> GetAllUserDiseases([FromRoute] Guid userId, [FromHeader] Guid token)
        {
            try
            {
                var result = await _userUseCase.GetUserDiseases(token, userId);

                return result?.Count switch
                {
                    null => Unauthorized(),
                    0 => NotFound(new
                        {message = "Could not find any disease for this user"}),
                    _ => Ok(result)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpGet]
        [Route("{userId}/diseases/{userDiseaseId}")]
        public async Task<IActionResult> GetUserDiseaseById([FromRoute] Guid userId, [FromRoute] Guid userDiseaseId,
            [FromHeader] Guid token)
        {
            try
            {
                var result = await _userUseCase.GetUserDiseaseById(token, userDiseaseId);
                return result switch
                {
                    null => Unauthorized(),
                    _ => Ok(result)
                };
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return NotFound(new {message = e.Message});
            }
        }

        [HttpPatch]
        [Route("{userId}/diseases/{userDiseaseId}")]
        public async Task<IActionResult> UpdateUserDisease([FromRoute] Guid userId, [FromRoute] Guid userDiseaseId,
            [FromHeader] Guid token, [FromBody] PatchDiseaseToUserDto dto)
        {
            try
            {
                var result = await _userUseCase.UpdateUserDisease(token, userId, userDiseaseId, dto);
                return result switch
                {
                    null => Unauthorized(),
                    _ => Ok(result)
                };
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                return NotFound(new {message = e.Message});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(new {message = e.Message});
            }
        }

        [HttpGet, Route("{userId}/issick")]
        public async Task<IActionResult> getAllAggregations([FromHeader]Guid token, [FromRoute]Guid userId)
        {
            var result = await this._userUseCase.IsSick(token, userId, null);
            return result switch
            {
                null => Unauthorized(),
                false => Ok(new{message = "You are not sick!"}),
                true => Ok(new{message = "You are sick!"})
            };
        }
    }
}

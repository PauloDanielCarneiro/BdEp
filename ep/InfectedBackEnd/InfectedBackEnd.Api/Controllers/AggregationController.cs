using System;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace InfectedBackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;

        public AggregationController(IUserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> getAllAggregations(Guid token, Guid userId)
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

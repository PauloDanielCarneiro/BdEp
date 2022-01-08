using System;
using System.Linq;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases.Locations;
using InfectedBackEnd.Application.UseCases.Locations.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InfectedBackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationsUseCase locationsUseCase;

        public LocationController(ILocationsUseCase locationsUseCase)
        {
            this.locationsUseCase = locationsUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations([FromHeader] Guid token)
        {
            var locationDto = await locationsUseCase.GetAllLocations(token);
            if (locationDto is null)
                return Unauthorized();
            else if (!locationDto.Any()) return NotFound(new {message = "No location found!"});

            return Ok(locationDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationRequestDto request,
            [FromHeader] Guid token)
        {
            var locationDto = await locationsUseCase.CreateLocation(request, token);
            return Ok(locationDto);
        }
    }
}
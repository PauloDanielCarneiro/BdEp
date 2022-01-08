using System;
using System.Linq;
using System.Threading.Tasks;
using InfectedBackEnd.Application.UseCases.Diseases;
using InfectedBackEnd.Application.UseCases.Diseases.DTO;
using InfectedBackEnd.Application.UseCases.Locations.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InfectedBackEnd.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseasesUseCase diseaseUseCase;

        public DiseaseController(IDiseasesUseCase diseaseUseCase)
        {
            this.diseaseUseCase = diseaseUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations([FromHeader] Guid token)
        {
            var diseaseDto = await diseaseUseCase.GetAllDiseases(token);
            if (diseaseDto is null)
                return Unauthorized();
            else if (!diseaseDto.Any()) return NotFound(new {message = "No disease found!"});

            return Ok(diseaseDto);
        }


        [HttpGet, Route("{name}")]
        public async Task<IActionResult> GetDiseaseByName([FromHeader] Guid token, [FromRoute] string name)
        {
            return Ok(await this.diseaseUseCase.GetDiseaseByName(token, name));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDisease([FromBody] CreateDiseaseRequestDto request,
            [FromHeader] Guid token)
        {
            try
            {
                var diseaseResponseDto = await diseaseUseCase.CreateDisease(request, token);
                return Ok(diseaseResponseDto);
            }
            catch (Exception e)
            {
                return BadRequest(new {message = e.Message});
            }
        }
    }
}

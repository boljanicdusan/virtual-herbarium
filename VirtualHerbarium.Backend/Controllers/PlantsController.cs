using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Services;

namespace VirtualHerbarium.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlantsController : ControllerBase
    {
        private readonly IPlantService _plantService;

        public PlantsController(IPlantService plantService)
        {
            _plantService = plantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlants(string vrsta, string porodica, string red, string staniste, string mjesto)
        {
            var includeDraft = User.Identity.IsAuthenticated;
            var result = await _plantService.GetAllPlants(vrsta, porodica, red, staniste, mjesto, includeDraft);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlantById(int id)
        {
            var result = await _plantService.GetPlantById(id);
            
            if (result == null)
            {
                return NotFound(new { Message = $"Plant with id {id} not found" });
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePlant([FromBody] CreatePlantDto input)
        {
            var result = await _plantService.CreatePlant(input);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, [FromBody] UpdatePlantDto input)
        {
            if (input.Id != id)
            {
                return BadRequest("Ids mismatch");
            }
            
            var result = await _plantService.UpdatePlant(input);
            if (result == null)
            {
                return NotFound(new { Message = $"Plant with id {id} not found" });
            }

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var result = await _plantService.DeletePlant(id);

            if (!result)
            {
                return NotFound(new { Message = $"Plant with id {id} not found" });
            }

            return Ok(new { Success = result });
        }
    }
}

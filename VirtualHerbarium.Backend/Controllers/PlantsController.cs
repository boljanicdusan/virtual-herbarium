using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;

        public PlantsController(IPlantService plantService, IWebHostEnvironment env)
        {
            _plantService = plantService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlants(string vrsta, string porodica, string red, string staniste, string mjesto)
        {
            var result = await _plantService.GetAllPlants(vrsta, porodica, red, staniste, mjesto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlantById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _plantService.CreatePlant(input);

            try
            {
                byte[] bytes = Convert.FromBase64String(input.SlikaBase64);
                var path = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", input.Slika);
                System.IO.File.WriteAllBytes(path, bytes);
            }
            catch (System.Exception)
            {
                // 
            }

            try
            {
                byte[] bytes = Convert.FromBase64String(input.SlikaUPrirodiBase64);
                var path = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", input.SlikaUPrirodi);
                System.IO.File.WriteAllBytes(path, bytes);
            }
            catch (System.Exception)
            {
                // 
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, [FromBody] UpdatePlantDto input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (input.Id != id)
            {
                return BadRequest("Ids mismatch");
            }

            var plant = await _plantService.GetPlantById(id);

            if (string.IsNullOrWhiteSpace(plant.Slika))
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(input.SlikaBase64);
                    var path = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", input.Slika);
                    System.IO.File.WriteAllBytes(path, bytes);
                }
                catch (System.Exception)
                {
                    // 
                }
            }

            if (string.IsNullOrWhiteSpace(plant.SlikaUPrirodi))
            {
                try
                {
                    byte[] bytes = Convert.FromBase64String(input.SlikaUPrirodiBase64);
                    var path = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", input.SlikaUPrirodi);
                    System.IO.File.WriteAllBytes(path, bytes);
                }
                catch (System.Exception)
                {
                    // 
                }
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _plantService.DeletePlant(id);

            if (!result)
            {
                return NotFound(new { Message = $"Plant with id {id} not found" });
            }

            return Ok(new { Success = result });
        }

        [Authorize]
        [HttpDelete("deleteimage/{id}")]
        public async Task<IActionResult> DeleteImage(int id, string type)
        {
            var plant = await _plantService.GetPlantById(id);

            if (plant == null)
            {
                return NotFound();
            }

            if (type == "slika")
            {
                var path = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", plant.Slika);
                System.IO.File.Delete(path);
            }
            if (type == "slikaUPrirodi")
            {
                var path = Path.Combine(_env.ContentRootPath, "wwwroot", "Images", plant.SlikaUPrirodi);
                System.IO.File.Delete(path);
            }

            var result = await _plantService.DeleteImageForPlant(id, type);
            return Ok(new { Success = result });
        }
    }
}
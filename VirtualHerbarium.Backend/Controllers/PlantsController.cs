using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Helpers;
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
                ImagesHelper.SaveImages(input.Slike, _env.ContentRootPath);
                // foreach (var slika in input.Slike)
                // {
                //     byte[] bytes = Convert.FromBase64String(slika.SlikaBase64);
                //     var path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", slika.Slika);
                //     System.IO.File.WriteAllBytes(path, bytes);
                // }
            }
            catch (System.Exception)
            {
                // 
            }

            try
            {
                ImagesHelper.SaveImages(input.SlikeUPrirodi, _env.ContentRootPath);
                // foreach (var slika in input.SlikeUPrirodi)
                // {
                //     byte[] bytes = Convert.FromBase64String(slika.SlikaBase64);
                //     var path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", slika.Slika);
                //     System.IO.File.WriteAllBytes(path, bytes);
                // }
                
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
            var result = await _plantService.UpdatePlant(input);

            var slikeToAdd = input.Slike.Where(s => !plant.Slike.Any(sb => sb.Slika == s.Slika)).ToList();
            var slikeUPrirodiToAdd = input.SlikeUPrirodi.Where(s => !plant.SlikeUPrirodi.Any(sb => sb.Slika == s.Slika)).ToList();
            var slikeToAddAll = slikeToAdd.Concat(slikeUPrirodiToAdd).ToList();

            var slikeToRemove = plant.Slike.Where(sb => !input.Slike.Any(s => s.Slika == sb.Slika)).ToList();
            var slikeUPrirodiToRemove = plant.SlikeUPrirodi.Where(sb => !input.SlikeUPrirodi.Any(s => s.Slika == sb.Slika)).ToList();
            var slikeToRemoveAll = slikeToRemove.Concat(slikeUPrirodiToRemove).ToList();

            await _plantService.CreatePlantImages(slikeToAddAll);
            await _plantService.DeletePlantImages(slikeToRemoveAll);

            try
            {
                ImagesHelper.SaveImages(slikeToAddAll, _env.ContentRootPath);
                DeleteImages(slikeToRemoveAll);
                // byte[] bytes = Convert.FromBase64String(input.SlikaBase64);
                // var path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", input.Slika);
                // System.IO.File.WriteAllBytes(path, bytes);
            }
            catch (System.Exception)
            {
                // 
            }

            // try
            // {
            //     byte[] bytes = Convert.FromBase64String(input.SlikaUPrirodiBase64);
            //     var path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", input.SlikaUPrirodi);
            //     System.IO.File.WriteAllBytes(path, bytes);
            // }
            // catch (System.Exception)
            // {
            //     // 
            // }

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

            // delete images from folder
            var plant = await _plantService.GetPlantById(id);
            if (plant != null)
            {
                // DeleteImage(plant.Slika);
                // DeleteImage(plant.SlikaUPrirodi);
                DeleteImages(plant.Slike);
                DeleteImages(plant.SlikeUPrirodi);
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
                // DeleteImage(plant.Slika);
            }
            if (type == "slikaUPrirodi")
            {
                // DeleteImage(plant.SlikaUPrirodi);
            }

            var result = await _plantService.DeleteImageForPlant(id, type);
            return Ok(new { Success = result });
        }

        private void DeleteImage(string image)
        {
            if (!string.IsNullOrWhiteSpace(image))
            {
                var path = Path.Combine(_env.ContentRootPath, "wwwroot", "images", image);
                try
                {
                    System.IO.File.Delete(path);
                }
                catch (System.Exception)
                {
                    // 
                }
            }
        }

        private void DeleteImages(List<PlantImageDto> plantImages)
        {
            var images = plantImages.Select(pi => pi.Slika);
            foreach (var image in images)
            {
                DeleteImage(image);
            }
        }
    }
}
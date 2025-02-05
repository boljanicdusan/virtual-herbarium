using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Entities;

namespace VirtualHerbarium.Backend.Services
{
    public class PlantService : IPlantService
    {
        private readonly VirtualHerbariumDbContext _context;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public PlantService(VirtualHerbariumDbContext context, IImageService imageService, IMapper mapper)
        {
            _context = context;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<List<PlantDto>> GetAllPlants(string vrsta, string porodica, string red, string staniste, string mjesto, bool includeDraft = false)
        {
            var query = _context.Plants.AsQueryable();

            if (!string.IsNullOrWhiteSpace(vrsta))
            {
                query = query.Where(p => p.Vrsta.Contains(vrsta) || p.TrivijalniNaziv.Contains(vrsta) || p.Sinonim.Contains(vrsta));
            }

            if (!string.IsNullOrWhiteSpace(porodica))
            {
                query = query.Where(p => p.Porodica.Contains(porodica));
            }

            if (!string.IsNullOrWhiteSpace(red))
            {
                query = query.Where(p => p.Red.Contains(red));
            }

            if (!string.IsNullOrWhiteSpace(staniste))
            {
                query = query.Where(p => p.LokacijeBiljaka.Any(lb => lb.Staniste.Contains(staniste)));
            }

            if (!string.IsNullOrWhiteSpace(mjesto))
            {
                query = query.Where(p => p.LokacijeBiljaka.Any(lb => lb.Mjesto.Contains(mjesto)));
            }

            if (!includeDraft)
            {
                query = query.Where(p => !p.IsDraft);
            }

            var plants = await query.OrderBy(b => b.Vrsta).ToListAsync();
            return _mapper.Map<List<Plant>, List<PlantDto>>(plants);
        }

        public async Task<PlantDto> GetPlantById(int id)
        {
            var plant = await _context.Plants
                .Include(p => p.SlikeBiljaka)
                .Include(p => p.LokacijeBiljaka)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plant != null)
            {
                return _mapper.Map<Plant, PlantDto>(plant);
            }

            return null;
        }

        public async Task<PlantDto> CreatePlant(CreatePlantDto input)
        {
            var plant = _mapper.Map<CreatePlantDto, Plant>(input);

            var slike = input.Slike.Select(s => new PlantImage(s.Slika, false)).ToList();
            var slikeUPrirodi = input.SlikeUPrirodi.Select(s => new PlantImage(s.Slika, true)).ToList();

            plant.SlikeBiljaka.AddRange(slike.Concat(slikeUPrirodi));

            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            try
            {
                _imageService.SaveImages(input.Slike);
                _imageService.SaveImages(input.SlikeUPrirodi);
            }
            catch (System.Exception)
            {
                // 
            }

            return _mapper.Map<Plant, PlantDto>(plant);
        }

        public async Task<PlantDto> UpdatePlant(UpdatePlantDto input)
        {
            var plant = await _context.Plants
                .Include(p => p.SlikeBiljaka)
                .Include(p => p.LokacijeBiljaka)
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            if (plant == null)
            {
                return null;
            }

            _mapper.Map(input, plant);

            var locationsToAdd = input.LokacijeBiljaka.Where(l => !plant.LokacijeBiljaka.Any(lb => lb.Id == l.Id)).ToList();
            var locationsToRemove = plant.LokacijeBiljaka.Where(lb => !input.LokacijeBiljaka.Any(l => l.Id == lb.Id)).ToList();

            await CreatePlantLocations(locationsToAdd);
            await DeletePlantLocations(locationsToRemove);

            var inputSlike = input.Slike.Concat(input.SlikeUPrirodi);
            var slikeToAdd = inputSlike.Where(s => !plant.SlikeBiljaka.Any(sb => sb.Slika == s.Slika)).ToList();
            var slikeToRemove = plant.SlikeBiljaka.Where(sb => !inputSlike.Any(s => s.Slika == sb.Slika)).ToList();

            await CreatePlantImages(slikeToAdd);
            await DeletePlantImages(slikeToRemove);

            await _context.SaveChangesAsync();

            _imageService.SaveImages(slikeToAdd);
            _imageService.DeleteImages(slikeToRemove);

            return _mapper.Map<PlantDto>(plant);
        }

        public async Task<bool> DeletePlant(int id)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);

            if (plant == null)
            {
                return false;
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            _imageService.DeleteImages(plant.SlikeBiljaka);

            return true;
        }

        private async Task CreatePlantImages(List<CreatePlantImageDto> images)
        {
            var plantImages = _mapper.Map<List<PlantImage>>(images);
            await _context.PlantImages.AddRangeAsync(plantImages);
        }

        private async Task DeletePlantImages(List<PlantImage> plantImages)
        {
            _context.PlantImages.RemoveRange(plantImages);
        }

        private async Task CreatePlantLocations(List<CreatePlantLocationDto> locations)
        {
            var plantLocations = _mapper.Map<List<PlantLocation>>(locations);
            await _context.PlantLocations.AddRangeAsync(plantLocations);
        }

        private async Task DeletePlantLocations(List<PlantLocation> plantLocations)
        {
            _context.RemoveRange(plantLocations);
        }
    }
}
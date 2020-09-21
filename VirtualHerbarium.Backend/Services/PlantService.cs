using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Entities;

namespace VirtualHerbarium.Backend.Services
{
    public class PlantService : IPlantService
    {
        private readonly VirtualHerbariumDbContext _context;
        private readonly IMapper _mapper;
        public PlantService(VirtualHerbariumDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PlantDto>> GetAllPlants(string vrsta, string porodica, string red, string staniste, string mjesto)
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
                query = query.Where(p => p.Staniste.Contains(staniste));
            }

            if (!string.IsNullOrWhiteSpace(mjesto))
            {
                query = query.Where(p => p.Mjesto.Contains(mjesto));
            }

            var plants = await query.ToListAsync();
            return _mapper.Map<List<Plant>, List<PlantDto>>(plants);
        }

        public async Task<PlantDto> GetPlantById(int id)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);

            if (plant != null)
            {
                return _mapper.Map<Plant, PlantDto>(plant);
            }

            return null;
        }

        public async Task<PlantDto> CreatePlant(CreatePlantDto input)
        {
            var plant = _mapper.Map<CreatePlantDto, Plant>(input);
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();
            return _mapper.Map<Plant, PlantDto>(plant);
        }

        public async Task<PlantDto> UpdatePlant(UpdatePlantDto input)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == input.Id);

            if (plant != null)
            {
                _mapper.Map<UpdatePlantDto, Plant>(input, plant);
                await _context.SaveChangesAsync();
                return _mapper.Map<Plant, PlantDto>(plant);
            }

            return null;
        }

        public async Task<bool> DeletePlant(int id)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);

            if (plant != null)
            {
                _context.Plants.Remove(plant);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteImageForPlant(int id, string type)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);

            if (plant == null)
            {
                return false;
            }

            if (type == "slika")
            {
                plant.Slika = null;
            }
            if (type == "slikaUPrirodi")
            {
                plant.SlikaUPrirodi = null;
            }

            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
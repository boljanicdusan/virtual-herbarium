using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualHerbarium.Backend.DTOs;

namespace VirtualHerbarium.Backend.Services
{
    public interface IPlantService
    {
        Task<List<PlantDto>> GetAllPlants(string vrsta, string porodica, string red, string staniste, string mjesto, bool includeDraft = false);
        Task<PlantDto> GetPlantById(int id);
        Task<PlantDto> CreatePlant(CreatePlantDto input);
        Task<PlantDto> UpdatePlant(UpdatePlantDto input);
        Task<bool> DeletePlant(int id);
    }
}
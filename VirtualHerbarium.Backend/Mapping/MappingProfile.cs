using AutoMapper;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Entities;

namespace VirtualHerbarium.Backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Model to DTO
            CreateMap<Plant, PlantDto>();
            CreateMap<User, UserDto>();

            // DTO to Model
            CreateMap<CreatePlantDto, Plant>();
            CreateMap<UpdatePlantDto, Plant>();
            
        }
    }
}
using System.Linq;
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
            CreateMap<User, UserDto>();
            CreateMap<PlantImage, PlantImageDto>();
            CreateMap<Plant, PlantDto>()
                .ForMember(dest => dest.Slike, opt => opt.MapFrom((src, d, role, context) => src.SlikeBiljaka.Where(s => !s.UPrirodi).Select(s => context.Mapper.Map<PlantImageDto>(s)).ToList()))
                .ForMember(dest => dest.SlikeUPrirodi, opt => opt.MapFrom((src, d, role, context) => src.SlikeBiljaka.Where(s => s.UPrirodi).Select(s => context.Mapper.Map<PlantImageDto>(s)).ToList()));

            // DTO to Model
            CreateMap<CreatePlantDto, Plant>();
            CreateMap<UpdatePlantDto, Plant>();
            CreateMap<CreatePlantImageDto, PlantImage>();
            CreateMap<PlantImageDto, PlantImage>();
        }
    }
}
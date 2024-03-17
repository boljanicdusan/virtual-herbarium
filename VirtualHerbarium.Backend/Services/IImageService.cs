using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Entities;

namespace VirtualHerbarium.Backend.Services
{
    public interface IImageService
    {
        void SaveImages(List<CreatePlantImageDto> images);
        void DeleteImage(string image);
        void DeleteImages(List<PlantImage> plantImages);
    }
}
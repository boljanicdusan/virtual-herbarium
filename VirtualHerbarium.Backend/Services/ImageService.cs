using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using VirtualHerbarium.Backend.DTOs;
using VirtualHerbarium.Backend.Entities;

namespace VirtualHerbarium.Backend.Services
{
    public class ImageService : IImageService
    {
        private readonly string _contentRootPath;
        
        public ImageService(IWebHostEnvironment env)
        {
            _contentRootPath = env.ContentRootPath;
        }
        
        public void SaveImages(List<CreatePlantImageDto> images)
        {
            foreach (var image in images)
            {
                byte[] bytes = Convert.FromBase64String(image.SlikaBase64);
                var path = Path.Combine(_contentRootPath, "wwwroot", "images", image.Slika);
                File.WriteAllBytes(path, bytes);
            }
        }

        public void DeleteImage(string image)
        {
            if (!string.IsNullOrWhiteSpace(image))
            {
                var path = Path.Combine(_contentRootPath, "wwwroot", "images", image);
                try
                {
                    File.Delete(path);
                }
                catch (System.Exception)
                {
                    // 
                }
            }
        }

        public void DeleteImages(List<PlantImage> plantImages)
        {
            var images = plantImages.Select(pi => pi.Slika);
            foreach (var image in images)
            {
                DeleteImage(image);
            }
        }
    }
}
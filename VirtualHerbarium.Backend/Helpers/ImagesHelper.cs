using System;
using System.Collections.Generic;
using System.IO;
using VirtualHerbarium.Backend.DTOs;

namespace VirtualHerbarium.Backend.Helpers
{
    public class ImagesHelper
    {
        public static void SaveImages(List<CreatePlantImageDto> images, string contentRootPath)
        {
            foreach (var image in images)
            {
                byte[] bytes = Convert.FromBase64String(image.SlikaBase64);
                var path = Path.Combine(contentRootPath, "wwwroot", "images", image.Slika);
                System.IO.File.WriteAllBytes(path, bytes);
            }
        }
    }
}
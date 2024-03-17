using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualHerbarium.Backend.DTOs
{
    public class PlantLocationDto
    {
        public int Id { get; set; }
        
        public string Staniste { get; set; }
        public string Mjesto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int BiljkaId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualHerbarium.Backend.Entities
{
    [Table("LokacijeBiljaka")]
    public class PlantLocation
    {
        public int Id { get; set; }
        
        public string Staniste { get; set; }
        public string Mjesto { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [ForeignKey("Biljka")]
        public int BiljkaId { get; set; }
        public Plant Biljka { get; set; }
    }
}
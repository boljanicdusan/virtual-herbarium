using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualHerbarium.Backend.Entities
{
    [Table("Biljke")]
    public class Plant
    {
        public Plant()
        {
            SlikeBiljaka = new List<PlantImage>();
        }
        
        public int Id { get; set; }
        public string Vrsta { get; set; }
        public string Porodica { get; set; }
        public string Red { get; set; }
        public string TrivijalniNaziv { get; set; }
        public string Sinonim { get; set; }
        public string Staniste { get; set; }
        public string Mjesto { get; set; }
        public string Opis { get; set; }
        // public string Slika { get; set; }
        // public string SlikaUPrirodi { get; set; }

        public List<PlantImage> SlikeBiljaka { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
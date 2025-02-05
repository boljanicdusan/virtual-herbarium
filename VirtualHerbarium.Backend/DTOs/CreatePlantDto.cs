using System.Collections.Generic;

namespace VirtualHerbarium.Backend.DTOs
{
    public class CreatePlantDto
    {
        public string Vrsta { get; set; }
        public string Porodica { get; set; }
        public string Red { get; set; }
        public string TrivijalniNaziv { get; set; }
        public string Sinonim { get; set; }
        public string Staniste { get; set; }
        public string Mjesto { get; set; }
        public string Opis { get; set; }
        public bool IsDraft { get; set; }
        // public string Slika { get; set; }
        // public string SlikaBase64 { get; set; }
        // public string SlikaUPrirodi { get; set; }
        // public string SlikaUPrirodiBase64 { get; set; }

        public List<CreatePlantImageDto> Slike { get; set; }
        public List<CreatePlantImageDto> SlikeUPrirodi { get; set; }
        public List<CreatePlantLocationDto> LokacijeBiljaka { get; set; }


        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
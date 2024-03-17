using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualHerbarium.Backend.Entities
{
    [Table("SlikeBiljaka")]
    public class PlantImage
    {
        public PlantImage()
        { }

        public PlantImage(string slika, bool uPrirodi)
        {
            Slika = slika;
            UPrirodi = uPrirodi;
        }
        
        public int Id { get; set; }

        public string Slika { get; set; }
        public bool UPrirodi { get; set; }

        [ForeignKey("Biljka")]
        public int BiljkaId { get; set; }
        public Plant Biljka { get; set; }
    }
}
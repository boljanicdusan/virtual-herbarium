namespace VirtualHerbarium.Backend.DTOs
{
    public class PlantImageDto
    {
        public int Id { get; set; }
        public string Slika { get; set; }
        public bool UPrirodi { get; set; }
        public int BiljkaId { get; set; }
    }
}
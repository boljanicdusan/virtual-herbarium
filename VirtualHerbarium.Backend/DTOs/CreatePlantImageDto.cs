namespace VirtualHerbarium.Backend.DTOs
{
    public class CreatePlantImageDto
    {
        public int Id { get; set; }
        public string Slika { get; set; }
        public string SlikaBase64 { get; set; }
        public bool UPrirodi { get; set; }
        public int BiljkaId { get; set; }
    }
}
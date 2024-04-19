namespace Application.Models.DTOs.ShoesDTOs
{
    public class GeneralShoeStatistics
    {
        public string ShoeId { get; set; }
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public byte Count { get; set; }
        public byte Size { get; set; }
        public string ImageUrl { get; set; }
    }
}
using Domain.Models;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class DetailsShoeStatisticsDto
    {
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public byte Count { get; set; }
        public byte Size { get; set; }
        public List<ShoeCountSize> ShoeCountSizes { get; set; }
        public string StoreId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}

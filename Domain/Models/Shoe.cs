using Domain.Models.Common;

namespace Domain.Models
{
    public class Shoe : BaseEntity
    {
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public string StoreId { get; set; }
        public List<ShoeCountSize> ShoeCountSize { get; set; } = new List<ShoeCountSize>();
        public List<string> ImageUrls { get; set; }
    }
}

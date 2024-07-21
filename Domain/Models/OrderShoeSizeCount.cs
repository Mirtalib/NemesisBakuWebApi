using Domain.Models.Common;

namespace Domain.Models
{
    public class OrderShoeSizeCount 
    {
        public Guid Id { get; set; }
        public byte Size { get; set; }
        public byte Count { get; set; }

        // Foreign Key With
        public Guid ShoeId { get; set; }


        // Navigation Property
        public Shoe Shoe { get; set; }
    }
}

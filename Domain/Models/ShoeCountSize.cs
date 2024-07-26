namespace Domain.Models
{
    public class ShoeCountSize 
    {
        public Guid Id { get; set; }
        public byte Size { get; set; }
        public byte Count { get; set; }

        // Foreign Key With
        public Guid ShoeId { get; set; }
        public Guid OrderId { get; set; }

        // Navigation Property

        public Shoe Shoe { get; set; }
        public Order Order { get; set; }
    }
}

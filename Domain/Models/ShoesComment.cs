using Domain.Models.Common;

namespace Domain.Models
{
    public class ShoesComment 
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }

        // Foreign Key With

        public Guid ShoeId { get; set; }
        public Guid ClientId { get; set; }


        // Navigation Property

        public Shoe Shoe { get; set; }
        public Client Client { get; set; }
    }
}

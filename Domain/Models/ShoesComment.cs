using Domain.Models.Common;

namespace Domain.Models
{
    public class ShoesComment :BaseEntity
    {
        public Shoe Shoe { get; set; }
        public Client Client { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}

using Domain.Models.Common;

namespace Domain.Models
{
    public class Store :BaseEntity
    {
        public string Name { get; set; }    
        public string Email { get; set; }    
        public string Description { get; set; }
        public List<string> ShoesId { get; set; }
        public List<string> OrderId { get; set; }
    }
}


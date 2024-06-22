using Domain.Models.Common;

namespace Domain.Models
{
    public class Store :BaseEntity
    {
        public string Name { get; set; }    
        public string Email { get; set; }    
        public string Description { get; set; }
        public List<string> Addresses { get; set; }
        public List<string> ShoesIds { get; set; }
        public List<string> OrderIds { get; set; }
        public List<string> CategoryIds { get;set; }
    }
}
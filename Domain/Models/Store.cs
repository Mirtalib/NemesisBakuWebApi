using Domain.Models.Common;

namespace Domain.Models
{
    public class Store :BaseEntity
    {
        public string Name { get; set; }    
        public string Email { get; set; }    
        public string Description { get; set; }
        public List<string> Addresses { get; set; }
        public List<Shoe> Shoes { get; set; }
        public List<Order> Orders { get; set; }
        public List<Category> Categorys { get;set; }
    }
}
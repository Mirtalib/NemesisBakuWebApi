using Domain.Models.Common;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public Store Store { get; set; }
        public string Name { get; set; }
        public List<Shoe> Shoes { get; set; }
    }
}

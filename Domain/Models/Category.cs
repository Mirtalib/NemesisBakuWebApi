using Domain.Models.Common;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<string> ShoesId { get; set; }
    }
}

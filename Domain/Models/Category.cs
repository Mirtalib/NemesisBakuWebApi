using Domain.Models.Common;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public string StoreId { get; set; }
        public string Name { get; set; }
        public List<string> ShoesId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }


        // Foreign Key With
        public Guid StoreId { get; set; }

        // Navigation Property
        public Store Store { get; set; }
        public List<Shoe> Shoes { get; set; }
    }
}

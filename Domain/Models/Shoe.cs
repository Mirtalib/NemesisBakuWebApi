using Domain.Models.Common;

namespace Domain.Models
{
    public class Shoe
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public List<string> ImageUrls { get; set; }



        // Foreign Key With
        public Guid CategoryId { get; set; }
        public Guid StoreId { get; set; }


        // Navigation Property

        public Category Category { get; set; }
        public Store Store { get; set; }

        public List<ClientFavoriShoes> ClientFavoriShoes { get; set; }
        public List<ClientShoeShoppingList> ClientShoppingList { get; set; }
        public List<ShoeCountSize> ShoeCountSizes { get; set; }
        public List<ShoesComment> ShoeComments { get; set; }
    }
}
namespace Domain.Models
{
    public class Client: AppUser
    {
        public string Address {  get; set; }


        // Navigation Property
        public List<ShoesComment> ShoesCommnets { get; set; }
        public List<Order> Orders { get; set; }
        public List<Shoe> ClientFavoriShoes { get; set; } 
        public List<Shoe> ClientShoppingList { get; set; } 
    }
}
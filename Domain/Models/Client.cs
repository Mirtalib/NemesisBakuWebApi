namespace Domain.Models
{
    public class Client: AppUser
    {
        public List<ShoesComment> ShoesCommnets { get; set; }
        public List<Order> Orders { get; set; }
        public List<Shoe> FavoriShoes { get; set; } 
        public List<Shoe> ShoppingList { get; set; } 
        public string Address {  get; set; } 
    }
}
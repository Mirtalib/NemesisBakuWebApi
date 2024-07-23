namespace Domain.Models
{
    public class Client: AppUser
    {
        public string Address {  get; set; }


        // Navigation Property
        public List<ShoesComment> ShoesCommnets { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderComment> OrderComments { get; set; }
        public List<ClientFavoriShoes> ClientFavoriShoes { get; set; } 
        public List<ClientShoeShoppingList> ClientShoppingList { get; set; } 
    }
}
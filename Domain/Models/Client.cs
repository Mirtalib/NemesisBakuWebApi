namespace Domain.Models
{
    public class Client: AppUser
    {
        public List<string> ShoesCommnetId { get; set; }
        public List<string> OrdersId { get; set; }
        public List<string> FavoriShoesIds { get; set; } 
    }
}
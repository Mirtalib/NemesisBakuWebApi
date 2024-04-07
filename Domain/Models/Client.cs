using Domain.Models.Common;

namespace Domain.Models
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<string> CommnetId { get; set; }
        public List<string> OrdersId { get; set; }
        public List<string> FavoriShoesIds { get; set; } 
    }
}
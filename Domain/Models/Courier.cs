namespace Domain.Models
{
    public class Courier :AppUser
    {        
        public byte Rate {  get; set; }

        // Navigation Property
        public List<Order> Orders { get; set; }
        public List<OrderComment> OrderComments { get; set; }
    }
}
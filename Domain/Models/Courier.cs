namespace Domain.Models
{
    public class Courier :AppUser
    {
        public List<Order> Orders { get; set; }
        public byte Rate {  get; set; }
    }
}

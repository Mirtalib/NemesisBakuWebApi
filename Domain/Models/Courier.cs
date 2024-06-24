namespace Domain.Models
{
    public class Courier :AppUser
    {
        public List<string> OrderIds { get; set; }

        public byte Rate {  get; set; }
    }
}

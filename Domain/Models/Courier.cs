namespace Domain.Models
{
    public class Courier :AppUser
    {
        public List<string> OrderIds { get; set; }
    }
}

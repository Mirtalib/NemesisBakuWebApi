using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public abstract class AppUser
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BrithDate { get; set; }
    }
}

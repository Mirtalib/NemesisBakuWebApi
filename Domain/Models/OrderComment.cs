using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class OrderComment
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }

        // Foreign Key With
        public Guid ClientId { get; set; }
        public Guid CourierId { get; set; }
        public Guid OrderId { get; set; }

        // Navigation Property
        public Order Order { get; set; }
        public Client Client { get; set; }
        public Courier Courier { get; set; }
    }
}
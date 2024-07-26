using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ClientShoeShoppingList
    {
        [Key]
        public Guid Id { get; set; }

        // Foreign Key With
        public Guid ClientId { get; set; }
        public Guid ShoeId { get; set; }


        // Navigation Property
        public Client Client { get; set; }
        public Shoe Shoe { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ClientShoeShoppingList
    {
        public Guid Id { get; set; }

        // Foreign Key With
        public Guid ClientId { get; set; }
        public Guid ShoeId { get; set; }


        // Navigation Property
        public Client Client { get; set; }
        public Shoe Shoe { get; set; }
    }
}

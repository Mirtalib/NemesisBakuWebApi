using Domain.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public DateTime OrderMakeTime { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }


        // Foreign Key With
        public Guid StoreId { get; set; }
        public Guid ClientId { get; set; }
        public Guid CourierId { get; set; }
        


        // Navigation Property
        public Store Store { get; set; }
        public Client Client { get; set; }
        public Courier Courier { get; set; }
        public OrderComment OrderComment { get; set; }
        public List<ShoeCountSize> Shoes { get; set; }
    }
}

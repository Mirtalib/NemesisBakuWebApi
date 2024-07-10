using Domain.Models.Common;
using Domain.Models.Enum;

namespace Domain.Models
{
    public class Order :BaseEntity
    {
        public Store Store { get; set; }
        public Client Client { get; set; }
        public Courier Courier { get; set; }
        public OrderComment OrderComment { get; set; }
        public List<OrderShoeSizeCount> Shoes { get; set; }
        public float Amount { get; set; }
        public DateTime OrderMakeTime { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

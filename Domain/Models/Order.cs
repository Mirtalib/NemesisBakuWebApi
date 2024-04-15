using Domain.Models.Common;
using Domain.Models.Enum;

namespace Domain.Models
{
    public class Order :BaseEntity
    {
        public string StoreId { get; set; }
        public List<string> ShoesIds   { get; set; }
        public string CourierId { get; set; }
        public string OrderCommentId { get; set; }
        public float Amount { get; set; }
        public DateTime OrderMakeTime { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
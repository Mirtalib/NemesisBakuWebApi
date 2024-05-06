using Domain.Models.Common;
using Domain.Models.Enum;

namespace Domain.Models
{
    public class Order :BaseEntity
    {
        public string StoreId { get; set; }
        public string ClientId { get; set; }
        public string CourierId { get; set; }
        public string OrderCommentId { get; set; }
        public List<OderShoeSizeCount> ShoesIds { get; set; }
        public float Amount { get; set; }
        public DateTime OrderMakeTime { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

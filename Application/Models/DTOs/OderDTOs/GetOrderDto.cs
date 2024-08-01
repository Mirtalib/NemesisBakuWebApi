using Domain.Models;
using Domain.Models.Enum;

namespace Application.Models.DTOs.OderDTOs
{
    public class GetOrderDto
    {
        public string Id { get; set; }
        public string StoreId { get; set; }
        public List<string> ShoesIds { get; set; }
        public string CourierId { get; set; }
        public string ClientId { get; set; }
        public string OrderCommentId { get; set; }
        public float Amount { get; set; }
        public DateTime OrderMakeTime { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

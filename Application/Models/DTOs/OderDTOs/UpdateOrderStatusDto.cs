using Domain.Models.Enum;

namespace Application.Models.DTOs.OderDTOs
{
    public class UpdateOrderStatusDto
    {
        public string OrderId { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}

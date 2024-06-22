using Domain.Models;
using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.OderDTOs
{
    public class GetOrderDto
    {
        public string Id { get; set; }
        public string StoreId { get; set; }
        public List<OderShoeSizeCount> ShoesIds { get; set; }
        public string CourierId { get; set; }
        public string ClientId { get; set; }
        public string OrderCommentId { get; set; }
        public float Amount { get; set; }
        public DateTime OrderMakeTime { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

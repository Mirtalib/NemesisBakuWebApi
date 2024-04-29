using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.OrderCommentDTOs
{
    public class GetOrderCommentDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public string CourierId { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}

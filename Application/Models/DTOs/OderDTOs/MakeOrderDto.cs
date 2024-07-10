using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.OderDTOs
{
    public class MakeOrderDto
    {
        public string StoreId { get; set; }
        public string ClientId { get; set; }
        public List<OrderShoeSizeCount> ShoesIds { get; set; }
    }
}

using Domain.Models.Common;
using Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order :BaseEntity
    {
        public string StoreId { get; set; }
        public List<string> ShoesIds   { get; set; }
        public float Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderFinishTime { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

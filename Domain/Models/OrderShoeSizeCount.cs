using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// OderShoeSizeCountConfiguration
namespace Domain.Models
{
    public class OrderShoeSizeCount :BaseEntity
    {
        public Shoe Shoe { get; set; }
        public byte Size { get; set; }
        public byte Count { get; set; }

    }
}

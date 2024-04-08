using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ShoesComment :BaseEntity
    {
        public string ShoesId { get; set; }
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}

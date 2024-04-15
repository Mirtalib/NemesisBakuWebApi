using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class OrderComment :BaseEntity
    {
        public string OrderId { get; set; }
        public string ClientId { get; set; }
        public string CourierId { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}
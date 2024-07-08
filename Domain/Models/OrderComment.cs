using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderComment :BaseEntity
    {
        public Order Order { get; set; }
        public Client Client { get; set; }
        public Courier Courier { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}
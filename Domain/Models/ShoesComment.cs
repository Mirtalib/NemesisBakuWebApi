using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ShoesComment
    {
        public string ShoesId { get; set; }
        public string ClientId { get; set; }
        public string Content { get; set; }
        public byte Rate { get; set; }
    }
}

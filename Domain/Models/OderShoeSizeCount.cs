using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OderShoeSizeCount :BaseEntity
    {
        public Shoe Shoe { get; set; }
        public byte Size { get; set; }
        public byte Count { get; set; }

    }
}

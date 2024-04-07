using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Shoes : BaseEntity
    {
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public KeyValuePair<byte, byte> ShoesSize { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}

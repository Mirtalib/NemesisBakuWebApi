using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class AddShoeDto
    {
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Color { get; set; }
        public string StoreId { get; set; }
        public float Price { get; set; }
        public List<KeyValuePair<byte, byte>> ShoesSize { get; set; } = new List<KeyValuePair<byte, byte>>();
        public IFormFile[] Images { get; set; } = new IFormFile[6];
    }
}

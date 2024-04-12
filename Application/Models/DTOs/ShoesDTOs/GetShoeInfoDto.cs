using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class GetShoeInfoDto
    {
        public string Id { get; set; }
        public string Model { get; set; }
        public string Brend { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Color { get; set; }
        public float Price { get; set; }
        public List<KeyValuePair<byte, byte>> ShoesSize { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}

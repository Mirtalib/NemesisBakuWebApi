using Domain.Models;
using Microsoft.AspNetCore.Http;

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
        public byte Count { get; set; }
        public byte Size { get; set; }
        public IFormFile[] Images { get; set; } = new IFormFile[6];
    }
}

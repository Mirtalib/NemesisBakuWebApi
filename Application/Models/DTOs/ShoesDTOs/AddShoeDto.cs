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
        public List<ShoeCountSize> ShoeCountSizes { get; set; }
    }
}


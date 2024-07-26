using Microsoft.AspNetCore.Http;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class UpdateShoeImageDto
    {
        public string ShoeId { get; set; }
        public IFormFile[] Images { get; set; } = new IFormFile[6];

    }
}

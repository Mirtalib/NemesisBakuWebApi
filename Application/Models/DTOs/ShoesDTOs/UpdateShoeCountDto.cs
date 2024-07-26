using Domain.Models;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class UpdateShoeCountDto
    {
        public List<ShoeCountSize> ShoeCountSizes { get; set; }
        public string ShoeId { get; set;}
    }
}

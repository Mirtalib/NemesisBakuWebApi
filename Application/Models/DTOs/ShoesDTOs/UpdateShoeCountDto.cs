using Application.Models.DTOs.ShoeCountSizeDTOs;
using Domain.Models;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class UpdateShoeCountDto
    {
        public List<UpdateShoeCountSizeDto> ShoeCountSizes { get; set; }
        public string ShoeId { get; set; }
    }
}
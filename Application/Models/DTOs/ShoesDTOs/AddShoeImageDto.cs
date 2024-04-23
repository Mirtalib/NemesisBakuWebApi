using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class AddShoeImageDto
    {
        public string ShoeId { get; set; }
        public IFormFile[] Images { get; set; } = new IFormFile[6];
    }
}
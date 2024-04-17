using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class UpdateShoeCountDto
    {
        public byte ShoeCount { get; set; }
        public string ShoeId { get; set;}
        public string StoreId { get; set; }
    }
}

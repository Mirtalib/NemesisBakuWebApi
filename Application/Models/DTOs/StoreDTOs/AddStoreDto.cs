using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.StoreDTOs
{
    public class AddStoreDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}

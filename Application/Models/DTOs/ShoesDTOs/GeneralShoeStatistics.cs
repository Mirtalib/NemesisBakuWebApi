using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs.ShoesDTOs
{
    public class GeneralShoeStatistics
    {
        public string ShoeId { get; set; }
        public string Model { get; set; }
        public string Brend { get; set; }
        public byte Size { get; set; }
    }
}
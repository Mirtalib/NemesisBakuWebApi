using Domain.Models.Common;

namespace Domain.Models
{
    public class ShoeCountSize
    {
        public Guid Id { get; set; }
        public byte Size { get; set; }
        public byte Count { get; set; }
    }
}
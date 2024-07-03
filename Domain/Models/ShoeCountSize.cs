using Domain.Models.Common;

namespace Domain.Models
{
    public class ShoeCountSize : BaseEntity
    {
        public byte Size { get; set; }
        public byte Count { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Enum
{
    public enum OrderStatus
    {
        Waiting = 0,
        Rejected = 1,
        Confirmed = 2,
        Packing = 3,
        OnTheWheels = 4,
        Delivered = 5,
        Rated = 6
    }
}
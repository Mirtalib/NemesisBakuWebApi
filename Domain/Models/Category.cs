﻿using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<string> ShoesId { get; set; }
    }
}

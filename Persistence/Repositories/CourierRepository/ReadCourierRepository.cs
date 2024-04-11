﻿using Application.IRepositories.ICourierRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CourierRepository
{
    public class ReadCourierRepository : ReadRepository<Courier> , IReadCourierRepository
    {
        public ReadCourierRepository(AppDbContext context)
            : base(context)
        {
            
        }
    }
}

﻿using Application.IRepositories.ICourierRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

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

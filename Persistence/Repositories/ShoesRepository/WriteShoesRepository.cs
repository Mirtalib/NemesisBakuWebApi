using Application.IRepositories.IShoesRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ShoesRepository
{
    public class WriteShoesRepository : WriteRepository<Shoes> , IWriteShoesRepository
    {
        public WriteShoesRepository(AppDbContext context)
            :base(context) 
        {
                
        }
    }
}

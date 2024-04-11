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
    public class ReadShoesRepository : ReadRepository<Shoes> , IReadShoesRepository 
    {
        public ReadShoesRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

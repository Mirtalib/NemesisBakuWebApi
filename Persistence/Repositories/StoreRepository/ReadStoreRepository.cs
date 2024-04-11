using Application.IRepositories.IStoreRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.StoreRepository
{
    public class ReadStoreRepository : ReadRepository<Store> , IReadStoreRepository
    {
        public ReadStoreRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

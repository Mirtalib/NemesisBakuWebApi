using Application.IRepositories.IStoreRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

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

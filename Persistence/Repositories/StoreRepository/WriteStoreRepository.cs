using Application.IRepositories.IStoreRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.StoreRepository
{
    public class WriteStoreRepository : WriteRepository<Store> , IWriteStoreRepository
    {
        public WriteStoreRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

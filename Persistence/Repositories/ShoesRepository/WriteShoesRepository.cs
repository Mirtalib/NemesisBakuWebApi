using Application.IRepositories.IShoesRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ShoesRepository
{
    public class WriteShoesRepository : WriteRepository<Shoe> , IWriteShoesRepository
    {
        public WriteShoesRepository(AppDbContext context)
            :base(context) 
        {
                
        }
    }
}

using Application.IRepositories.IShoesRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ShoesRepository
{
    public class ReadShoesRepository : ReadRepository<Shoe> , IReadShoesRepository 
    {
        public ReadShoesRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

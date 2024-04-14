using Application.IRepositories.ICourierRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.CourierRepository
{
    public class WriteCourierRepository : WriteRepository<Courier> , IWriteCourierRepository
    {
        public WriteCourierRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

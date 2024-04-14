using Application.IRepositories.IAdminRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.AdminRepository
{
    public class ReadAdminRepository : ReadRepository<Admin> , IReadAdminRepository
    {
        public ReadAdminRepository(AppDbContext context)
            :base(context) 
        {
                
        }
    }
}

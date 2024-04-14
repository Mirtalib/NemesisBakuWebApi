using Application.IRepositories.IAdminRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.AdminRepository
{
    public class WriteAdminRepository : WriteRepository<Admin> , IWriteAdminRepository
    {
        public WriteAdminRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

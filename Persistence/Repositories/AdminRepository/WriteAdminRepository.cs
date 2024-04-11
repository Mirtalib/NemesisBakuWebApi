using Application.IRepositories.IAdminRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

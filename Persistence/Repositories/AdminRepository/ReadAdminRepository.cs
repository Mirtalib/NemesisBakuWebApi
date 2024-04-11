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
    public class ReadAdminRepository : ReadRepository<Admin> , IReadAdminRepository
    {
        public ReadAdminRepository(AppDbContext context)
            :base(context) 
        {
                
        }
    }
}

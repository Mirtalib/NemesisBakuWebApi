using Application.IRepositories.ICategoryRepository;
using Application.IRepositories.Repository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CategoryRepository
{
    public class ReadCategoryRepository : ReadRepository<Category> , IReadCategoryRepository
    {
        public ReadCategoryRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

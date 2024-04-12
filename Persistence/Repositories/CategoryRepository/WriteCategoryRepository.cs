using Application.IRepositories.ICategoryRepository;
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
    public class WriteCategoryRepository : WriteRepository<Category> , IWriteCategoryRepository
    {
        public WriteCategoryRepository(AppDbContext context)
            :base(context)
        {
                

        }
    }
}

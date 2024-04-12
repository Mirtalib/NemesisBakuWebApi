using Application.IRepositories.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.ICategoryRepository
{
    public interface IWriteCategoryRepository : IWriteRepository<Category>
    {

    }
}

using Application.IRepositories.Repository;
using Domain.Models;

namespace Application.IRepositories.ICategoryRepository
{
    public interface IReadCategoryRepository : IReadRepository<Category>
    {
    }
}

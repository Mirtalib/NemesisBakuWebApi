using System.Linq.Expressions;
using Domain.Models.Common;

namespace Application.IRepositories.Repository
{
    public interface IReadRepository<T> where T : class
    {
        IEnumerable<T?> GetAll(bool tracking = true);

        IEnumerable<T?> GetWhere(Expression<Func<T, bool>> expression);

        Task<T?> GetAsync(string id);

        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
    }

}

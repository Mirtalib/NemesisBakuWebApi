using Application.IRepositories.Repository;
using Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Application.IRepositories.IRepository
{
    public interface IWriteRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        
        Task AddRangeAsync(IEnumerable<T> entities);

        bool Update(T entity);
     
        Task<bool> UpdateAsync(string Id);

        bool Remove(T entity);
        
        Task<bool> RemoveAsync(string Id);


        Task<int> SaveChangesAsync();

    }

}

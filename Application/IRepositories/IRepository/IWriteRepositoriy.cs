using Domain.Models.Common;



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

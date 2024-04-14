using Application.IRepositories.IRepository;
using Domain.Models;

namespace Application.IRepositories.IAdminRepository
{
    public interface IWriteAdminRepository : IWriteRepository<Admin>
    {

    }
}

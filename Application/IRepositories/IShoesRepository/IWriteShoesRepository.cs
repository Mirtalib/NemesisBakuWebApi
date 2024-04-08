using Application.IRepositories.IRepository;
using Domain.Models;

namespace Application.IRepositories.IShoesRepository
{
    public interface IWriteShoesRepository :IWriteRepository<Shoes>
    {

    }
}
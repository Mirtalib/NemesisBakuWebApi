using Application.IRepositories.IRepository;
using Domain.Models;

namespace Application.IRepositories.IOrderRepository
{
    public interface IWriteOrderRepository :IWriteRepository<Order>
    {

    }
}

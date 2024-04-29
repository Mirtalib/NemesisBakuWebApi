using Application.IRepositories.IOrderCommentRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.OrderCommentRepository
{
    public class ReadOrderCommentRepository : ReadRepository<OrderComment> , IReadOrderCommentRepository
    {
        public ReadOrderCommentRepository(AppDbContext context)
            :base(context) 
        {
            
        }
    }
}

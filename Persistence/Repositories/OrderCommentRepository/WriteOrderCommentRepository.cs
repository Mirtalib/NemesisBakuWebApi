using Application.IRepositories.IOrderCommentRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.OrderCommentRepository
{
    public class WriteOrderCommentRepository : WriteRepository<OrderComment> , IWriteOrderCommentRepository
    {
        public WriteOrderCommentRepository(AppDbContext context)
            :base(context) 
        {
                
        }
    }
}

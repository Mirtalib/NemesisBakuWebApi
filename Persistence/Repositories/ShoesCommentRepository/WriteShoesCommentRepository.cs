using Application.IRepositories.IShoesCommentRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ShoesCommentRepository
{
    public class WriteShoesCommentRepository : WriteRepository<ShoesComment> , IWriteShoesCommentRepository
    {
        public WriteShoesCommentRepository(AppDbContext context)
            :base(context)
        {
            
        }

    }
}

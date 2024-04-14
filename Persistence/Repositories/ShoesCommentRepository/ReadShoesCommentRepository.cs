using Application.IRepositories.IShoesCommentRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ShoesCommentRepository
{
    public class ReadShoesCommentRepository : ReadRepository<ShoesComment> , IReadShoesCommentRepository
    {
        public ReadShoesCommentRepository(AppDbContext context)
            :base(context)
        {
                
        }
    }
}

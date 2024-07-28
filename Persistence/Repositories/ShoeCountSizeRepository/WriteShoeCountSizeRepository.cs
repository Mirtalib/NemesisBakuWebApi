using Application.IRepositories.IShoeCountSizeRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ShoeCountSizeRepository
{
    public class WriteShoeCountSizeRepository : WriteRepository<ShoeCountSize>, IWriteShoeCountSizeRepository
    {
        public WriteShoeCountSizeRepository(AppDbContext context)
            : base(context)
        {

        }

    }
}

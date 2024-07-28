using Application.IRepositories.IShoeCountSizeRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ShoeCountSizeRepository
{
    public class ReadShoeCountSizeRepository : ReadRepository<ShoeCountSize>, IReadShoeCountSizeRepository
    {
        public ReadShoeCountSizeRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}
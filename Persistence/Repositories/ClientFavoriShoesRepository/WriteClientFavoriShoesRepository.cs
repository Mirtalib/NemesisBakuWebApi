using Application.IRepositories.IClientFavoriShoesRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ClientFavoriShoesRepository
{
    public class WriteClientFavoriShoesRepository : WriteRepository<ClientFavoriShoes>, IWriteClientFavoriShoesRepository
    {
        public WriteClientFavoriShoesRepository(AppDbContext context)
            : base(context)
        {


        }
    }
}

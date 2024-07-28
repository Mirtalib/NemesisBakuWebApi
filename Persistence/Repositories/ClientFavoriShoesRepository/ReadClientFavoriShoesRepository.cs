using Application.IRepositories.IClientFavoriShoesRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ClientFavoriShoesRepository
{
    public class ReadClientFavoriShoesRepository : ReadRepository<ClientFavoriShoes>, IReadClientFavoriShoesRepository
    {
        public ReadClientFavoriShoesRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}

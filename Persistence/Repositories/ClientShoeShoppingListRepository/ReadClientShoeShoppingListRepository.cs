using Application.IRepositories.IClientShoeShoppingListRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ClientShoeShoppingListRepository
{
    public class ReadClientShoeShoppingListRepository : ReadRepository<ClientShoeShoppingList>, IReadClientShoeShoppingListRepository
    {
        public ReadClientShoeShoppingListRepository(AppDbContext context) : base(context)
        {

        }
    }
}

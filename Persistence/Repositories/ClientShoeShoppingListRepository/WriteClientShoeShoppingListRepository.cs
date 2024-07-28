using Application.IRepositories.IClientShoeShoppingListRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ClientShoeShoppingListRepository
{
    public class WriteClientShoeShoppingListRepository : WriteRepository<ClientShoeShoppingList>, IWriteClientShoeShoppingListRepository
    {
        public WriteClientShoeShoppingListRepository(AppDbContext context) : base(context)
        {

        }
    }
}
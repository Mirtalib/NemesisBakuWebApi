using Application.IRepositories.IClientRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;



namespace Persistence.Repositories.ClientRepository
{
    public class WriteClientRepository : WriteRepository<Client> , IWriteClientRepository
    {
        public WriteClientRepository(AppDbContext context):base(context)
        {
                
        }
    }
}

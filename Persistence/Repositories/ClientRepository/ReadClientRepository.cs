using Application.IRepositories.IClientRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;

namespace Persistence.Repositories.ClientRepository
{
    public class ReadClientRepository :ReadRepository<Client> , IReadClientRepository
    {
        public ReadClientRepository(AppDbContext context):base(context) 
        {

        }
    }
}

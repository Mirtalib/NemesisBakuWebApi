using Application.IRepositories.IClientRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ClientRepository
{
    public class ReadClientRepository :ReadRepository<Client> , IReadClientRepository
    {
        public ReadClientRepository(AppDbContext context):base(context) 
        {

        }
    }
}

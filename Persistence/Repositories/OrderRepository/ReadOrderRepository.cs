using Application.IRepositories.IOrderRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.OrderRepository
{
    public class ReadOrderRepository : ReadRepository<Order>, IReadOrderRepository
    {
        public ReadOrderRepository(AppDbContext context)
            : base(context)
        {

        }
    }
}

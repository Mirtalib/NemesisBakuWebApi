﻿using Application.IRepositories.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories.IOrderCommentRepository
{
    public interface IReadOrderCommentRepository : IReadRepository<OrderComment>
    {

    }
}

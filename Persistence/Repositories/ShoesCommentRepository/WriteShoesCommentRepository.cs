﻿using Application.IRepositories.IShoesCommentRepository;
using Domain.Models;
using Persistence.Context;
using Persistence.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ShoesCommentRepository
{
    public class WriteShoesCommentRepository : WriteRepository<ShoesComment> , IWriteShoesCommentRepository
    {
        public WriteShoesCommentRepository(AppDbContext context)
            :base(context)
        {
            
        }

    }
}
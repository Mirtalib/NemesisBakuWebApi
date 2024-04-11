﻿using Application.IRepositories;
using Application.IRepositories.IAdminRepository;
using Application.IRepositories.IClientRepository;
using Application.IRepositories.ICourierRepository;
using Application.IRepositories.IOrderRepository;
using Application.IRepositories.IShoesCommentRepository;
using Application.IRepositories.IShoesRepository;
using Application.IRepositories.IStoreRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IReadAdminRepository readAdminRepository, IWriteAdminRepository writeAdminRepository, IReadClientRepository readClientRepository, IWriteClientRepository writeClientRepository, IReadCourierRepository readCourierRepository, IWriteCourierRepository writeCourierRepository, IReadShoesRepository readShoesRepository, IWriteShoesRepository writeShoesRepository, IReadShoesCommentRepository readShoesCommentRepository, IWriteShoesCommentRepository writeShoesCommentRepository, IReadStoreRepository readStoreRepository, IWriteStoreRepository writeStoreRepository, IReadOrderRepository readOrderRepository, IWriteOrderRepository writeOrderRepository)
        {
            ReadAdminRepository = readAdminRepository;
            WriteAdminRepository = writeAdminRepository;
            ReadClientRepository = readClientRepository;
            WriteClientRepository = writeClientRepository;
            ReadCourierRepository = readCourierRepository;
            WriteCourierRepository = writeCourierRepository;
            ReadShoesRepository = readShoesRepository;
            WriteShoesRepository = writeShoesRepository;
            ReadShoesCommentRepository = readShoesCommentRepository;
            WriteShoesCommentRepository = writeShoesCommentRepository;
            ReadStoreRepository = readStoreRepository;
            WriteStoreRepository = writeStoreRepository;
            ReadOrderRepository = readOrderRepository;
            WriteOrderRepository = writeOrderRepository;
        }

        public IReadAdminRepository ReadAdminRepository { get; }

        public IWriteAdminRepository WriteAdminRepository { get; }

        public IReadClientRepository ReadClientRepository { get; }

        public IWriteClientRepository WriteClientRepository { get; }

        public IReadCourierRepository ReadCourierRepository { get; }

        public IWriteCourierRepository WriteCourierRepository { get; }

        public IReadShoesRepository ReadShoesRepository { get; }

        public IWriteShoesRepository WriteShoesRepository { get; }

        public IReadShoesCommentRepository ReadShoesCommentRepository { get; }

        public IWriteShoesCommentRepository WriteShoesCommentRepository { get; }

        public IReadStoreRepository ReadStoreRepository { get; }

        public IWriteStoreRepository WriteStoreRepository { get; }

        public IReadOrderRepository ReadOrderRepository { get; }

        public IWriteOrderRepository WriteOrderRepository { get; }
    }
}

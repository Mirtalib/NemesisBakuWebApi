using Application.IRepositories.IAdminRepository;
using Application.IRepositories.ICategoryRepository;
using Application.IRepositories.IClientRepository;
using Application.IRepositories.ICourierRepository;
using Application.IRepositories.IOrderRepository;
using Application.IRepositories.IShoesCommentRepository;
using Application.IRepositories.IShoesRepository;
using Application.IRepositories.IStoreRepository;

namespace Application.IRepositories
{
    public interface IUnitOfWork
    {
        IReadAdminRepository ReadAdminRepository { get; }
        IWriteAdminRepository WriteAdminRepository { get; }

        IReadClientRepository ReadClientRepository { get; }
        IWriteClientRepository WriteClientRepository { get;}

        IReadCourierRepository ReadCourierRepository { get; }
        IWriteCourierRepository WriteCourierRepository { get; }

        IReadShoesRepository ReadShoesRepository { get; }
        IWriteShoesRepository WriteShoesRepository { get; }

        IReadShoesCommentRepository ReadShoesCommentRepository { get; }
        IWriteShoesCommentRepository WriteShoesCommentRepository { get; }

        IReadStoreRepository ReadStoreRepository { get; }
        IWriteStoreRepository WriteStoreRepository { get; }

        IReadOrderRepository ReadOrderRepository { get; }
        IWriteOrderRepository WriteOrderRepository { get;}

        IReadCategoryRepository ReadCategoryRepository { get; }
        IWriteCategoryRepository WriteCategoryRepository { get; }
    }
}

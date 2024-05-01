using Application.IRepositories;
using Application.Services.IHelperServices;
using Application.Services.IUserServices;

namespace Infrastructure.Services.UserServices
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobService _blobSerice;

        public ClientService(IUnitOfWork unitOfWork, IBlobService blobSerice)
        {
            _unitOfWork = unitOfWork;
            _blobSerice = blobSerice;
        }

    }
}

using Application.IRepositories;
using Application.Models.DTOs.OderDTOs;
using Application.Services.IHelperServices;
using Application.Services.IUserServices;
using Domain.Models;

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


        public async Task<bool> MakeOrder(MakeOrderDto dto)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(dto.StoreId);
            if (store is null)
                throw new ArgumentNullException();
            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            if (client is null)
                throw new ArgumentNullException();

            var newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = dto.ClientId,
                StoreId = store.Id,
                OrderMakeTime = DateTime.UtcNow,
                OrderStatus = Domain.Models.Enum.OrderStatus.Waiting,
            };

            foreach (var shoeId in dto.ShoesIds)
            {
                var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId.ShoeId);
                foreach (var item in shoe.ShoeCountSizes)
                {
                    var x = dto.ShoesIds.FirstOrDefault(x => x.Size == item.Size && x.Count - item.Count < 0);
                    if (x == default)
                        throw new ArgumentException();

                    item.Count -= x.Count;

                    newOrder.ShoesIds.Add(x);
                    await _unitOfWork.WriteShoesRepository.UpdateAsync(shoe.Id);
                    await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
                }
            }

            store.OrderIds.Add(newOrder.Id);
            client.OrdersId.Add(newOrder.Id);

            await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            await _unitOfWork.WriteClientRepository.UpdateAsync(client.Id);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            var result =  await _unitOfWork.WriteOrderRepository.AddAsync(newOrder);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();

            return result;
        }

    }
}

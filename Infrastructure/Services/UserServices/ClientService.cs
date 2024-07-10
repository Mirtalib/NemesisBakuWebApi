using Application.IRepositories;
using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Services.IHelperServices;
using Application.Services.IUserServices;
using Domain.Models;

namespace Infrastructure.Services.UserServices
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        #region Shoe 

        public async Task<List<GetShoeDto>> GetAllShoes(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");

            var shoesDto = new List<GetShoeDto>();
            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(x => x.Store.Id == storeId).ToList();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id,
                        Brend = shoe.Brend,
                        ImageUrls = shoe.ImageUrls,
                        Model = shoe.Model,
                        Price = shoe.Price,
                    });
            }

            return shoesDto;
        }


        public async Task<List<GetShoeDto>> GetShoeByCategoryId(string categoryId)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(categoryId);
            if (category is null)
                throw new ArgumentNullException($"Category {categoryId}");


            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(shoe => shoe.Category.Id == categoryId);

            var shoesDto = new List<GetShoeDto>();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id,
                        Brend = shoe.Brend,
                        ImageUrls = shoe.ImageUrls,
                        Model = shoe.Model,
                        Price = shoe.Price,
                    });
            }

            return shoesDto;
        }


        public async Task<GetShoeInfoDto> GetShoeId(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            var shoeDto = new GetShoeInfoDto
            {
                Id = shoe.Id,
                Brend = shoe.Brend,
                ImageUrls = shoe.ImageUrls,
                Model = shoe.Model,
                Price = shoe.Price,
                CategoryId = shoe.Category.Id,
                Color = shoe.Color,
                Description = shoe.Description,
            };

            return shoeDto;
        }


        #endregion


        #region Favori List


        public async Task<List<GetShoeDto>> GetFavoriteList(string clientId)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(clientId);
            if (client is null)
                throw new ArgumentNullException();

            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(shoe => client.FavoriShoes.Contains(shoe));


            var shoesDto = new List<GetShoeDto>();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id,
                        Brend = shoe.Brend,
                        ImageUrls = shoe.ImageUrls,
                        Model = shoe.Model,
                        Price = shoe.Price,
                    });
            }

            return shoesDto;
        }


        public async Task<bool> AddToShoeFavoriteList(AddFavoriteListDto dto)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            if (client is null)
                throw new ArgumentNullException();
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException();


            client.FavoriShoes.Add(shoe);

            var result = _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }


        public async Task<bool> RemoveToShoeFavoriteList(RemoveFavoriteListDto dto)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            if (client is null)
                throw new ArgumentNullException();
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException();


            client.FavoriShoes.Remove(shoe);

            var result = _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;

        }

        #endregion


        #region Order

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
                // Client = dto.ClientId,
                Store = store,
                OrderMakeTime = DateTime.UtcNow,
                OrderStatus = Domain.Models.Enum.OrderStatus.Waiting,
            };

            foreach (var shoeId in dto.ShoesIds)
            {
                var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId.Shoe.Id);
                if (shoe is not null)
                    foreach (var item in shoe.ShoeCountSizes)
                    {
                        var x = dto.ShoesIds.FirstOrDefault(x => x.Size == item.Size && x.Count - item.Count < 0);
                        if (x == default)
                            throw new ArgumentException();

                        item.Count -= x.Count;

                        newOrder.Shoes.Add(x);
                        await _unitOfWork.WriteShoesRepository.UpdateAsync(shoe.Id);
                        await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
                    }
            }

            store.Orders.Add(newOrder);
            client.Orders.Add(newOrder);

            _unitOfWork.WriteStoreRepository.Update(store);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            var result =  await _unitOfWork.WriteOrderRepository.AddAsync(newOrder);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();

            return result;
        }


        public List<GetOrderDto> GetAllOrder(string clientId)
        {
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(order => order.Client.Id == clientId);
            if (orders.Count() == 0)
                throw new ArgumentNullException();

            var ordersDto = new List<GetOrderDto>();
            foreach (var order in orders)
            {
                if (order is not null)
                {
                    var orderDto = new GetOrderDto
                    {
                        Id = order.Id,
                        StoreId = order.Store.Id,
                        CourierId = order.Courier.Id,
                        OrderCommentId = order.OrderComment.Id,
                        Amount = order.Amount,
                        OrderFinishTime = order.OrderFinishTime,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                        ShoesIds = new List<OrderShoeSizeCount>()
                    };
                    orderDto.ShoesIds.AddRange(order.Shoes);

                    ordersDto.Add(orderDto);
                }
            }
            return ordersDto;
        }


        public async Task<GetOrderDto> GetOrder(string orderId)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order == null)
                throw new ArgumentNullException("");

            var orderDto = new GetOrderDto
            {
                Id = orderId,
                StoreId = order.Store.Id,
                CourierId = order.Courier.Id,
                ClientId = order.Client.Id,
                Amount = order.Amount,
                OrderCommentId = order.OrderComment.Id,
                OrderFinishTime = order.OrderFinishTime,
                OrderMakeTime = order.OrderMakeTime,
                OrderStatus = order.OrderStatus,
            };

            orderDto.ShoesIds.AddRange(order.Shoes);

            return orderDto;

        }
        #endregion


        #region ShoppingList


        public async Task<List<GetShoeDto>> GetShoppingList(string clientId)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(clientId);
            if (client is null)
                throw new ArgumentNullException();

            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(shoe => client.ShoppingList.Contains(shoe));

            var shoesDto = new List<GetShoeDto>();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id,
                        Brend = shoe.Brend,
                        ImageUrls = shoe.ImageUrls,
                        Model = shoe.Model,
                        Price = shoe.Price,
                    });
            }

            return shoesDto;
        }


        public async Task<bool> AddToShoeShoppingList(AddShoppingListDto dto)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            if (client is null)
                throw new ArgumentNullException();
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException();


            client.ShoppingList.Add(shoe);

            var result = _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }


        public async Task<bool> RemoveToShoeShoppingList(RemoveShoppingListDto dto)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            if (client is null)
                throw new ArgumentNullException();
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException();


            client.ShoppingList.Remove(shoe);

            var result = _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }
        #endregion

    }
}

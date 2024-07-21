using Application.IRepositories;
using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using System.Drawing;

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
            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(x => x.Store.Id.ToString() == storeId).ToList();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id.ToString(),
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


            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(shoe => shoe.Category.Id.ToString() == categoryId);

            var shoesDto = new List<GetShoeDto>();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id.ToString(),
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
                Id = shoe.Id.ToString(),
                Brend = shoe.Brend,
                ImageUrls = shoe.ImageUrls,
                Model = shoe.Model,
                Price = shoe.Price,
                CategoryId = shoe.Category.Id.ToString(),
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

            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(shoe => client.ClientFavoriShoes.Contains(shoe));


            var shoesDto = new List<GetShoeDto>();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id.ToString(),
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


            client.ClientFavoriShoes.Add(shoe);

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


            client.ClientFavoriShoes.Remove(shoe);

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
                Id = Guid.NewGuid(),
                // Client = dto.ClientId,
                Store = store,
                OrderMakeTime = DateTime.UtcNow,
                OrderStatus = Domain.Models.Enum.OrderStatus.Waiting,
            };

            foreach (var shoeId in dto.ShoesIds)
            {
                var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId.Shoe.Id.ToString());
                if (shoe is not null)
                    foreach (var item in shoe.ShoeCountSizes)
                    {
                        var x = dto.ShoesIds.FirstOrDefault(x => x.Size == item.Size && x.Count - item.Count < 0);
                        if (x == default)
                            throw new ArgumentException();

                        item.Count -= x.Count;

                        newOrder.Shoes.Add(x);
                        _unitOfWork.WriteShoesRepository.Update(shoe);
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
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(order => order.Client.Id.ToString() == clientId);
            if (orders.Count() == 0)
                throw new ArgumentNullException();

            var ordersDto = new List<GetOrderDto>();
            foreach (var order in orders)
            {
                if (order is not null)
                {
                    var orderDto = new GetOrderDto
                    {
                        Id = order.Id.ToString(),
                        StoreId = order.Store.Id.ToString(),
                        CourierId = order.Courier.Id.ToString(),
                        OrderCommentId = order.OrderComment.Id.ToString(),
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
                StoreId = order.Store.Id.ToString(),
                CourierId = order.Courier.Id.ToString(),
                ClientId = order.Client.Id.ToString(),
                Amount = order.Amount,
                OrderCommentId = order.OrderComment.Id.ToString(),
                OrderFinishTime = order.OrderFinishTime,
                OrderMakeTime = order.OrderMakeTime,
                OrderStatus = order.OrderStatus,
            };

            orderDto.ShoesIds.AddRange(order.Shoes);

            return orderDto;

        }
        #endregion


        #region Shopping List


        public async Task<List<GetShoeDto>> GetShoppingList(string clientId)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(clientId);
            if (client is null)
                throw new ArgumentNullException();

            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(shoe => client.ClientShoppingList.Contains(shoe));

            var shoesDto = new List<GetShoeDto>();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id.ToString(),
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


            client.ClientShoppingList.Add(shoe);

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


            client.ClientShoppingList.Remove(shoe);

            var result = _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

        #endregion


        #region Profile

        public async Task<GetClientProfileDto> GetProfile(string clientId)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(clientId);
            if (client is null)
                throw new ArgumentNullException();

            var clientDto = new GetClientProfileDto
            {
                Id = client.Id.ToString(),
                Name = client.Name,
                Surname = client.Surname,
                PhoneNumber = client.PhoneNumber,
                BrithDate = client.BrithDate,
                Email = client.Email,
                Address = client.Address,
            };
            return clientDto;
        }

        public async Task<bool> RemoveProfile(string clientId)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(clientId);
            if (client is null)
                throw new ArgumentNullException();

            foreach (var shoesComment in client.ShoesCommnets)
            {
                var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoesComment.Id.ToString());
                if (shoe is null)
                    throw new ArgumentNullException();

                shoe.ShoeComments.Remove(shoesComment);

                _unitOfWork.WriteShoesRepository.Update(shoe);
                await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
            }


            foreach (var order in client.Orders)
            {
                order.Client = new();

                _unitOfWork.WriteOrderRepository.Update(order);
                await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
            }


            var result = _unitOfWork.WriteClientRepository.Remove(client);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateProfile(UpdateClientProfileDto dto)
        {

            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.Id);
            if (client is null)
                throw new ArgumentNullException();

            client.Name = dto.Name;
            client.Surname = dto.Surname;
            client.Email = dto.Email;
            client.BrithDate = dto.BrithDate;
            client.PhoneNumber = dto.PhoneNumber;
            client.Address = dto.Address;

            var result = _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            return result;
        }

        #endregion


        #region Shoe Comment


        public async Task<bool> CreateShoeComment(CreateShoeCommentDto dto)
        {
            var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            if (client is null)
                throw new ArgumentNullException();

            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException();


            foreach (var order in client.Orders)
            {
                foreach (var orderShoe in order.Shoes)
                {
                    if (orderShoe.Id == shoe.Id)
                    {
                        var shoeComment = new ShoesComment
                        {
                            Id = Guid.NewGuid(),
                            Client = client,
                            Shoe = shoe,
                            Content = dto.Content,
                            Rate = dto.Rate,
                        };
                        var result = await _unitOfWork.WriteShoesCommentRepository.AddAsync(shoeComment);
                        await _unitOfWork.WriteShoesCommentRepository.SaveChangesAsync();
                        return result;
                    }
                }
            }


            throw new ArgumentException();
        }

        public async Task<bool> RemoveShoeComment(string shoeCommentId)
        {

            var shoeComment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(shoeCommentId);
            if (shoeComment is null)
                throw new ArgumentNullException();


            shoeComment.Client.ShoesCommnets.Remove(shoeComment);

            _unitOfWork.WriteClientRepository.Update(shoeComment.Client);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            var result = _unitOfWork.WriteShoesCommentRepository.Remove(shoeComment);
            await _unitOfWork.WriteShoesCommentRepository.SaveChangesAsync();

            return result;
        }

        #endregion
    }
}

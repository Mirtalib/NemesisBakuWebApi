using Application.IRepositories;
using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using System.Collections.Generic;

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

            foreach (var countSizes in shoe.ShoeCountSizes)
                shoeDto.ShoeCountSizeIds.Add(countSizes.Id.ToString());


            return shoeDto;
        }


        #endregion


        #region Favori List


        public List<GetShoeDto> GetFavoriteList(string clientId)
        {

            var favorishoes = _unitOfWork.ReadClientFavoriShoesRepository.GetWhere(shoe => shoe.ClientId.ToString() == clientId);
            if (favorishoes is null)
                throw new ArgumentNullException();


            var shoesDto = new List<GetShoeDto>();
            foreach (var favorishoe in favorishoes)
                if (favorishoe is not null)
                {
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = favorishoe.Shoe.Id.ToString(),
                        Brend = favorishoe.Shoe.Brend,
                        ImageUrls = favorishoe.Shoe.ImageUrls,
                        Model = favorishoe.Shoe.Model,
                        Price = favorishoe.Shoe.Price,
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

            var favorishoe = new ClientFavoriShoes
            {
                Id = Guid.NewGuid(),
                ClientId = client.Id,
                ShoeId = shoe.Id,
            };


            var result = await _unitOfWork.WriteClientFavoriShoesRepository.AddAsync(favorishoe);
            await _unitOfWork.WriteClientFavoriShoesRepository.SaveChangesAsync();
            return result;
        }


        public async Task<bool> RemoveToShoeFavoriteList(string favoriShoeId)
        {
            var favorishoe = await _unitOfWork.ReadClientFavoriShoesRepository.GetAsync(favoriShoeId);
            if (favorishoe is null)
                throw new ArgumentNullException();


            var result = _unitOfWork.WriteClientFavoriShoesRepository.Remove(favorishoe);
            await _unitOfWork.WriteClientFavoriShoesRepository.SaveChangesAsync();
            return result;
        }

        #endregion


        #region Order

        public async Task<bool> MakeOrder(MakeOrderDto dto)
        {
            //var store = await _unitOfWork.ReadStoreRepository.GetAsync(dto.StoreId);
            //if (store is null)
            //    throw new ArgumentNullException();
            //var client = await _unitOfWork.ReadClientRepository.GetAsync(dto.ClientId);
            //if (client is null)
            //    throw new ArgumentNullException();
            


            //var newOrder = new Order
            //{
            //    Id = Guid.NewGuid(),
            //    // Client = dto.ClientId,
            //    Store = store,
            //    OrderMakeTime = DateTime.UtcNow,
            //    OrderStatus = Domain.Models.Enum.OrderStatus.Waiting,
            //};

            //foreach (var shoeId in dto.ShoesIds)
            //{
            //    var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId.Shoe.Id.ToString());
            //    if (shoe is not null)
            //        foreach (var item in shoe.ShoeCountSizes)
            //        {
            //            var x = dto.ShoesIds.FirstOrDefault(x => x.Size == item.Size && x.Count - item.Count < 0);
            //            if (x == default)
            //                throw new ArgumentException();

            //            item.Count -= x.Count;

            //            newOrder.Shoes.Add(x);
            //            _unitOfWork.WriteShoesRepository.Update(shoe);
            //            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
            //        }
            //}

            //store.Orders.Add(newOrder);
            //client.Orders.Add(newOrder);

            //_unitOfWork.WriteStoreRepository.Update(store);
            //await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            //_unitOfWork.WriteClientRepository.Update(client);
            //await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            //var result =  await _unitOfWork.WriteOrderRepository.AddAsync(newOrder);
            //await _unitOfWork.WriteOrderRepository.SaveChangesAsync();

            //return result;

            throw new NotImplementedException();
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
                        ShoesIds = new List<string>()
                    };
                    foreach (var shoe in order.Shoes)
                        orderDto.ShoesIds.Add(shoe.Id.ToString());

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

            foreach (var shoe in order.Shoes)
                orderDto.ShoesIds.Add(shoe.Id.ToString());

            return orderDto;

        }
        #endregion


        #region Shopping List


        public async Task<List<GetShoeDto>> GetShoppingList(string clientId)
        {
            var shoppingLists = _unitOfWork.ReadClientShoeShoppingListRepository.GetWhere(shoe => shoe.ClientId.ToString() == clientId);
            if (shoppingLists is null)
                throw new ArgumentNullException();


            var shoesDto = new List<GetShoeDto>();
            foreach (var shoppingShoe in shoppingLists)
                if (shoppingShoe is not null)
                {
                    var shoe =await _unitOfWork.ReadShoesRepository.GetAsync(shoppingShoe.ShoeId.ToString());
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


            var shoppingList = new ClientShoeShoppingList
            {
                Id = Guid.NewGuid(),
                ShoeId = shoe.Id,
                ClientId = client.Id,
            };

            var result = await _unitOfWork.WriteClientShoeShoppingListRepository.AddAsync(shoppingList);
            await _unitOfWork.WriteClientShoeShoppingListRepository.SaveChangesAsync();
            
            return result;
        }


        public async Task<bool> RemoveToShoeShoppingList(string shoppingShoeId)
        {
            var shoppingShoe = await _unitOfWork.ReadClientShoeShoppingListRepository.GetAsync(shoppingShoeId);
            if (shoppingShoe is null)
                throw new ArgumentNullException();


            var result = _unitOfWork.WriteClientShoeShoppingListRepository.Remove(shoppingShoe);
            await _unitOfWork.WriteClientShoeShoppingListRepository.SaveChangesAsync();
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

        public async Task<GetShoeCommentDto> GetShoeComment(string commentId)
        {
            var shoeComment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(commentId);
            if (shoeComment is null)
                throw new ArgumentNullException();

            var shoeCommentDto = new GetShoeCommentDto
            {
                Id = shoeComment.Id.ToString(),
                ClientId = shoeComment.ClientId.ToString(),
                ShoesId = shoeComment.ShoeId.ToString(),
                Content = shoeComment.Content,
                Rate = shoeComment.Rate,
            };

            return shoeCommentDto;
        }

        public List<GetShoeCommentDto> GetAllShoeComment(string clientId)
        {
            var comments = _unitOfWork.ReadShoesCommentRepository.GetWhere(client => client.Id.ToString() == clientId);
            if (comments.Count() == 0)
                throw new ArgumentNullException();

            var shoeCommnetDtos  = new List<GetShoeCommentDto>();
            foreach (var comment in comments)
                if (comment is not null)
                    shoeCommnetDtos.Add(new GetShoeCommentDto
                    {
                        Id = comment.Id.ToString(),
                        ClientId = comment.ClientId.ToString(),
                        ShoesId = comment.ShoeId.ToString(),
                        Content = comment.Content,
                        Rate = comment.Rate,
                    });
            return shoeCommnetDtos;
        }




        #endregion
    }
}

using Application.IRepositories;
using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IHelperServices;
using Application.Services.IUserServices;
using Application.Models.DTOs.OrderCommentDTOs;
using Domain.Models;
using Domain.Models.Enum;
using FluentValidation;
using Application.Models.DTOs.ShoesCommentDTOs;

namespace Infrastructure.Services.UserServices
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobService _blobSerice;
        private readonly IValidator<AddShoeDto> _addShoeValidator;

        public StoreService(IUnitOfWork unitOfWork, IBlobService blobSerice, IValidator<AddShoeDto> addShoeValidator)
        {
            _unitOfWork = unitOfWork;
            _blobSerice = blobSerice;
            _addShoeValidator = addShoeValidator;
        }

        #region Shoe
        public async Task<bool> CreateShoe(AddShoeDto shoeDto)
        {
            var isValid = _addShoeValidator.Validate(shoeDto);
            if (isValid.IsValid)
            {
                var store = await _unitOfWork.ReadStoreRepository.GetAsync(shoeDto.StoreId);
                if (store is null)
                    throw new ArgumentNullException("Store is not found");

                var category = await _unitOfWork.ReadCategoryRepository.GetAsync(shoeDto.CategoryId);
                if (category is null)
                    throw new ArgumentNullException("Category is not found");

                

                var newShoe = new Shoe
                {
                    Id = Guid.NewGuid(),
                    Brend = shoeDto.Brend,
                    Model = shoeDto.Model,
                    Category = category,
                    Color = shoeDto.Color,
                    Description = shoeDto.Description,
                    Store = store,
                    ImageUrls = new List<string>(),
                    Price = shoeDto.Price,
                    ShoeComments = new List<ShoesComment>(),
                    ShoeCountSizes = new List<ShoeCountSize>()
                    
                };

                foreach (var item in shoeDto.ShoeCountSizes)
                    if (item is not null)
                    {
                        var shoeCountSize = new ShoeCountSize
                        {
                            Id = Guid.NewGuid(),
                            Count = item.Count,
                            ShoeId = newShoe.Id,
                            Shoe = newShoe,
                            Size = item.Size,
                        };
                        newShoe.ShoeCountSizes.Add(shoeCountSize);

                        await _unitOfWork.WriteShoeCountSizeRepository.AddAsync(shoeCountSize);
                    }

                store.Shoes.Add(newShoe);
                category.Shoes.Add(newShoe);


                await _unitOfWork.WriteShoeCountSizeRepository.SaveChangesAsync();

                await _unitOfWork.WriteShoesRepository.AddAsync(newShoe);
                await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

                _unitOfWork.WriteStoreRepository.Update(store);
                await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

                _unitOfWork.WriteCategoryRepository.Update(category);
                await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

                return true;
            }
            throw new ValidationException("No Valid");
        }


        public async Task<bool> CreateShoeImages(AddShoeImageDto dto)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null || shoe.ImageUrls.Count != 0)
                throw new ArgumentNullException("Shoe not found");

            for (int i = 0; i < dto.Images.Length; i++)
            {
                var form = dto.Images[i];
                using (var stream = form.OpenReadStream())
                {
                    var fileName = shoe.Id + "-" + shoe.Model + shoe.Color + i + ".jpg";
                    var contentType = form.ContentType;

                    var blobResult = await _blobSerice.UploadFileAsync(stream, fileName, contentType);
                    if (blobResult is false)
                        return false;

                    shoe.ImageUrls.Add(_blobSerice.GetSignedUrl(fileName));
                }
            }

            _unitOfWork.WriteShoesRepository.Update(shoe);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateShoeImage(UpdateShoeImageDto dto)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null || shoe.ImageUrls.Count != 0)
                throw new ArgumentNullException("Shoe not found");



            for (int i = 0; i < shoe.ImageUrls.Count; i++)
                await _blobSerice.DeleteFileAsync(shoe.Id + "-" + shoe.Model + shoe.Color + i + ".jpg");


            for (int i = 0; i < dto.Images.Length; i++)
            {
                var form = dto.Images[i];
                using (var stream = form.OpenReadStream())
                {
                    var fileName = shoe.Id + "-" + shoe.Model + shoe.Color + i + ".jpg";
                    var contentType = form.ContentType;

                    var blobResult = await _blobSerice.UploadFileAsync(stream, fileName, contentType);
                    if (blobResult is false)
                        return false;

                    shoe.ImageUrls.Add(_blobSerice.GetSignedUrl(fileName));
                }
            }

            var result = _unitOfWork.WriteShoesRepository.Update(shoe);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
            return result;
        }


        public async Task<bool> UpdateShoeCount(UpdateShoeCountDto dto)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            shoe.ShoeCountSizes.ForEach(async x =>
            {
                foreach (var item in dto.ShoeCountSizes)
                {
                    if (x.Size == item.Size)
                    {
                        x.Count += item.Count;
                        _unitOfWork.WriteShoeCountSizeRepository.Update(x);
                        await _unitOfWork.WriteShoeCountSizeRepository.SaveChangesAsync();
                    }
                }
            });

            var result = _unitOfWork.WriteShoesRepository.Update(shoe);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateShoe(UpdateShoeDto dto)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.Id);
            if (shoe is null)
                throw new ArgumentNullException();
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(dto.CategoryId);
            if (category is null)
                throw new ArgumentNullException();
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(dto.StoreId);
            if (store is null)
                throw new ArgumentNullException();


            shoe.Description = dto.Description;
            shoe.Price = dto.Price;
            shoe.Brend = dto.Brend;
            shoe.Model = dto.Model;
            shoe.Category = category;
            shoe.Store = store;
            shoe.Color = dto.Color;

            var result = _unitOfWork.WriteShoesRepository.Update(shoe);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> RemoveShoe(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            var store = await _unitOfWork.ReadStoreRepository.GetAsync(shoe.Store.Id.ToString());
            if (store is null)
                throw new ArgumentNullException("Store is not found");

            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(shoe.Category.Id.ToString());
            if (category is null)
                throw new ArgumentNullException("Category is not found");



            for (int i = 0; i < shoe.ImageUrls.Count; i++)
                await _blobSerice.DeleteFileAsync(shoe.Id + "-" + shoe.Model + shoe.Color + i + ".jpg");

            store.Shoes.Remove(shoe);
            category.Shoes.Remove(shoe);



            _unitOfWork.WriteCategoryRepository.Update(category);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            _unitOfWork.WriteStoreRepository.Update(store);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            var result = _unitOfWork.WriteShoesRepository.Remove(shoe);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

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


        #region Profile
        public async Task<GetStoreProfileDto> GetProfile(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");
            var storeDto = new GetStoreProfileDto
            {
                Id = store.Id.ToString(),
                Description = store.Description,
                Email = store.Email,
                Name = store.Name,
                Addresses = store.Addresses,
                PhoneNumbers = store.PhoneNumbers
            };

            return storeDto;
        }

        public async Task<bool> UptadeStore(UpdateStoreDto dto)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(dto.Id);
            if (store is null)
                throw new ArgumentNullException("Wrong Store");

            store.Name = dto.Name;
            store.Description = dto.Description;
            store.Email = dto.Email;
            store.PhoneNumbers = dto.PhoneNumbers;
            store.Addresses = dto.Addresses;

            var result =  _unitOfWork.WriteStoreRepository.Update(store);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> RemoveStore(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Wrong Store");


            foreach (var category in store.Categorys)
            {
                await RemoveCategory(category.Id.ToString());
            }

            foreach (var order in store.Orders)
            {
                await RemoveOrder(order.Id.ToString());
            }

            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();

            _unitOfWork.WriteStoreRepository.Remove(store);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();
            return true;
        }
    #endregion


        #region Category

        public async Task<bool> CreateCategory(CreateCategoryDto dto)
        {
            var testCategory = await _unitOfWork.ReadCategoryRepository.GetAsync(x => x.Name.ToLower() == dto.Name.ToLower());
            if (testCategory is not null)
                throw new ArgumentException("Wrong Name");

            var store = await _unitOfWork.ReadStoreRepository.GetAsync(dto.StoreId);
            if (store is null)
                throw new ArgumentNullException();

            var newCategory = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Shoes = new List<Shoe>(),
                Store = store
            };

            var result = await _unitOfWork.WriteCategoryRepository.AddAsync(newCategory);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> RemoveCategory(string categoryId)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(categoryId);
            if (category is null)
                throw new ArgumentNullException("Wrong Category Id");

            foreach (var shoe in category.Shoes)
            {
                await RemoveShoe(shoe.Id.ToString());
            }

            var result = _unitOfWork.WriteCategoryRepository.Remove(category);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateCategory(UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(dto.Id);
            if (category is null)
                throw new ArgumentNullException();

            category.Name = dto.Name;

            var result = _unitOfWork.WriteCategoryRepository.Update(category);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();
            return result;
        }


        public async Task<GetCategoryDto> GetCategory(string categoryId)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(categoryId.ToString());
            if (category is null)
                throw new ArgumentNullException("Wrong Category Id");

            var categoryDto = new GetCategoryDto
            {
                Id = categoryId.ToString(),
                Name = category.Name,
            };

            foreach (var shoe in category.Shoes)
                categoryDto.ShoesIds.Add(shoe.Id.ToString());


            return categoryDto;
        }


        public List<GetCategoryDto> GetAllCategory()
        {
            var categories = _unitOfWork.ReadCategoryRepository.GetAll();
            if (categories.Count() is 0)
                throw new ArgumentNullException("Wrong No Category");


            var categoriesDto = new List<GetCategoryDto>();
            foreach (var category in categories)
            {
                if (category is not null)
                {
                    var categoryDto = new GetCategoryDto
                    {
                        Id = category.Id.ToString(),
                        Name = category.Name,
                    };
                    foreach (var shoe in category.Shoes)
                        categoryDto.ShoesIds.Add(shoe.Id.ToString());
                    categoriesDto.Add(categoryDto);
                }
            }
            return categoriesDto;
        }

#endregion


        #region Order

        public async Task<bool> InLastDecidesSituation(InLastSituationOrderDto orderDto)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderDto.OrderId);
            if (order is null)
                throw new ArgumentNullException();

            if (orderDto.IsLast)
            {
                order.OrderStatus = OrderStatus.Confirmed;
                var result = _unitOfWork.WriteOrderRepository.Update(order);
                await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
                return result;
            }

            order.OrderStatus = OrderStatus.Rejected;
            _unitOfWork.WriteOrderRepository.Update(order);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
            return true;
        }


        public async Task<GetOrderDto> GetOrder(string orderId)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order is null)
                throw new ArgumentNullException();

            var orderDto = new GetOrderDto
            {
                Id = orderId,
                StoreId = order.Store.Id.ToString(),
                CourierId = order.Courier.Id.ToString(),
                OrderCommentId = order.OrderComment.Id.ToString(),
                ClientId = order.Client.Id.ToString(),
                Amount = order.Amount,
                OrderFinishTime = order.OrderFinishTime,
                OrderMakeTime = order.OrderMakeTime,
                OrderStatus = order.OrderStatus,
            };
            foreach (var shoe in order.Shoes)
                orderDto.ShoesIds.Add(shoe.Id.ToString());

            return orderDto;
        }


        public async Task<List<GetOrderDto>> GetAllOrder(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException();

            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x => store.Orders.Contains(x));

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
                        ClientId = order.Client.Id.ToString(),
                        OrderCommentId = order.OrderComment.Id.ToString(),
                        Amount = order.Amount,                        
                        OrderFinishTime = order.OrderFinishTime,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                    };

                    foreach (var shoe in order.Shoes)
                        orderDto.ShoesIds.Add(shoe.Id.ToString());
                    
                    ordersDto.Add(orderDto);
                }
            }
            return ordersDto;
        }


        public async Task<List<GetOrderDto>> GetActiveOrder(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException();
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x => store.Orders.Contains(x) && x.OrderStatus != OrderStatus.Rated);


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
                        ClientId = order.Client.Id.ToString(),
                        Amount = order.Amount,
                        OrderFinishTime = default,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                    };
                    foreach (var shoe in order.Shoes)
                        orderDto.ShoesIds.Add(shoe.Id.ToString());

                    ordersDto.Add(orderDto);
                }
            }
            return ordersDto;
        }


        public async Task<bool> UpdateOrderStatus(UpdateOrderStatusDto orderDto)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderDto.OrderId);
            if (order is null)
                throw new ArgumentNullException();

            order.OrderStatus = orderDto.OrderStatus;

            var result = _unitOfWork.WriteOrderRepository.Update(order);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
            return result;
        }


        public async Task<bool> RemoveOrder(string orderId)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order is null)
                throw new ArgumentNullException();

            var store = await _unitOfWork.ReadStoreRepository.GetAsync(order.Store.Id.ToString());
            if (store is null)
                throw new ArgumentNullException();

            var client = await _unitOfWork.ReadClientRepository.GetAsync(order.Client.Id.ToString());
            if (client is null)
                throw new ArgumentNullException();

            var courier = await _unitOfWork.ReadCourierRepository.GetAsync(order.Courier.Id.ToString());
            if (courier is null)
                throw new ArgumentNullException();


            store.Orders.Remove(order);
            client.Orders.Remove(order);
            courier.Orders.Remove(order);

            _unitOfWork.WriteStoreRepository.Update(store);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            _unitOfWork.WriteCourierRepository.Update(courier);
            await _unitOfWork.WriteCourierRepository.SaveChangesAsync();

            _unitOfWork.WriteOrderCommentRepository.Remove(order.OrderComment);
            await _unitOfWork.WriteOrderCommentRepository.SaveChangesAsync();

            var result = _unitOfWork.WriteOrderRepository.Remove(order);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();

            return result;
        }


        #endregion


        #region Order Comment

        public async Task<GetOrderCommentDto> GetOrderComment(string orderCommentId)
        {
            var orderComment = await _unitOfWork.ReadOrderCommentRepository.GetAsync(orderCommentId);
            if (orderComment is null)
                throw new ArgumentNullException();


            var orderCommentDto = new GetOrderCommentDto
            {
                Id = orderComment.Id.ToString(),
                ClientId = orderComment.Client.Id.ToString(),
                Content = orderComment.Content,
                CourierId = orderComment.Courier.Id.ToString(),
                OrderId = orderComment.Order.Id.ToString(),
                Rate = orderComment.Rate,
            };

            return orderCommentDto;
        }

        public List<GetOrderCommentDto> GetAllOrderComment()
        {
            var orderComments = _unitOfWork.ReadOrderCommentRepository.GetAll();

            var orderCommentDto = new List<GetOrderCommentDto>();
            foreach (var orderComment in orderComments)
            {
                if (orderComment is not null)
                    orderCommentDto.Add(new GetOrderCommentDto
                    {
                        ClientId = orderComment.Id.ToString(),
                        Content = orderComment.Content,
                        CourierId = orderComment.Courier.Id.ToString(),
                        Id = orderComment.Order.Id.ToString(),
                        Rate = orderComment.Rate,
                        OrderId = orderComment.Order.Id.ToString(),
                    });
            }
            return orderCommentDto;
        }

        public async Task<bool> RemoveOrderComment(string orderCommentId)
        {
            var orderComment = await _unitOfWork.ReadOrderCommentRepository.GetAsync(orderCommentId);
            if (orderComment is null)
                throw new ArgumentNullException("");

            var result = _unitOfWork.WriteOrderCommentRepository.Remove(orderComment);
            await _unitOfWork.WriteOrderCommentRepository.SaveChangesAsync();
            return result;
        }


        #endregion


        #region Shoe Comment


        public async Task<List<GetShoeCommentDto>> GetAllShoeComment(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException();

            var commentsDto = new List<GetShoeCommentDto>();

            foreach (var commentId in shoe.ShoeComments)
            {
                var comment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(commentId.Id.ToString());
                if (comment is not null)
                    commentsDto.Add(new GetShoeCommentDto
                    {
                        Id = comment.Id.ToString(),
                        ClientId = comment.Client.Id.ToString(),
                        ShoesId = comment.Shoe.Id.ToString(),
                        Content = comment.Content,
                        Rate = comment.Rate,
                    });
            }

            return commentsDto;

        }

        public async Task<GetShoeCommentDto> GetShoeComment(string commentId)
        {
            var comment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(commentId);
            if (comment is null)
                throw new ArgumentNullException();

            var commentDto = new GetShoeCommentDto
            {
                Id = comment.Id.ToString(),
                ClientId = comment.Client.Id.ToString(),
                ShoesId = comment.Shoe.Id.ToString(),
                Content = comment.Content,
                Rate = comment.Rate,
            };
            return commentDto;
        }

        public async Task<bool> RemoveShoeComment(string commentId)
        {
            var comment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(commentId);
            if (comment is null)
                throw new ArgumentNullException();

            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(comment.Shoe.Id.ToString());
            if (shoe is null)
                throw new ArgumentNullException();

            var client = await _unitOfWork.ReadClientRepository.GetAsync(comment.Client.Id.ToString());
            if (client is null)
                throw new ArgumentNullException();

            shoe.ShoeComments.Remove(comment);
            client.ShoesCommnets.Remove(comment);


            _unitOfWork.WriteClientRepository.Update(client);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            _unitOfWork.WriteShoesRepository.Update(shoe);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            var result = _unitOfWork.WriteShoesCommentRepository.Remove(comment);
            await _unitOfWork.WriteShoesCommentRepository.SaveChangesAsync();

            return result;
        }

        #endregion


        #region ShoeSalesStatistics

        public List<GeneralShoeStatisticsDto> WeeklySalesStatistics(string storeId)
        {
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x => x.Store.Id.ToString() == storeId && x.OrderMakeTime > DateTime.Now.AddDays(-7).Date).ToList();
            if (orders.Count is 0)
                throw new ArgumentNullException("Order not Found");

            var shoesDto = new List<GeneralShoeStatisticsDto>();
            var ShoesIds = new List<string>();
            foreach (var order in orders)
                if (order is not null)
                    foreach (var shoe in order.Shoes)
                        ShoesIds.Add(shoe.Id.ToString());

            var shoes = _unitOfWork.ReadShoesRepository.GetAll();
            foreach (var item in shoes)
            {
                if (item is not null)
                {
                    var shoe = ShoesIds.Where(x => x == item.Id.ToString()).ToList();
                    if (shoe.Count != 0)
                        shoesDto.Add(new GeneralShoeStatisticsDto
                        {
                            ShoeId = item.Id.ToString(),
                            Brend = item.Brend,
                            Model = item.Model,
                            Color = item.Color,
                            Price = item.Price,
                            Count = Convert.ToByte(shoe.Count),
                            ImageUrl = item.ImageUrls[0]
                        });
                }
            }
            return shoesDto;
        }


        //public async Task<List<DetailsShoeStatisticsDto>> DetailsShoeStatistics(string shoeId)
        //{
        //    //var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
        //    //if (shoe is null)
        //    //    throw new ArgumentNullException();

        //    //var orders  = _unitOfWork.ReadOrderRepository.GetWhere(order => order.ShoesIds.Contains(shoe.ShoeCountSizes));
        //    //if (orders.Count() == 0)
        //    //    throw new ArgumentNullException();


        //    //foreach (var order in orders)
        //    //{
        //    //    if (order is not null)
        //    //    {


        //    //    }
        //    //}

        //    throw new NotImplementedException();
        //}



        #endregion
    }
}


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
using Microsoft.AspNetCore.Components.Forms;

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
            var isValid  = _addShoeValidator.Validate(shoeDto);
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
                    Id = Guid.NewGuid().ToString(),
                    Brend = shoeDto.Brend,
                    Model = shoeDto.Model,
                    CategoryId = shoeDto.CategoryId,
                    Color = shoeDto.Color,
                    Description = shoeDto.Description,
                    StoreId = shoeDto.StoreId,
                    ImageUrls = new List<string>(),
                    Price = shoeDto.Price,
                };

                newShoe.ShoeCountSizes.AddRange(shoeDto.ShoeCountSizes);

                store.ShoesIds.Add(newShoe.Id);
                category.ShoesId.Add(newShoe.Id);

                await _unitOfWork.WriteShoesRepository.AddAsync(newShoe);
                await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

                await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
                await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

                await _unitOfWork.WriteCategoryRepository.UpdateAsync(category.Id);
                await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

                return true;
            }
            throw new ValidationException("No Valid");
        }

        public async Task<bool> AddShoeImages(AddShoeImageDto dto)
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

            await _unitOfWork.WriteShoesRepository.UpdateAsync(shoe.Id);
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

            var result =  await _unitOfWork.WriteShoesRepository.UpdateAsync(shoe.Id);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> RemoveShoe(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            var store = await _unitOfWork.ReadStoreRepository.GetAsync(shoe.StoreId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");

            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(shoe.CategoryId);
            if (category is null)
                throw new ArgumentNullException("Category is not found");



            for (int i = 0; i < shoe.ImageUrls.Count; i++)
                await _blobSerice.DeleteFileAsync(shoe.Id + "-" + shoe.Model + shoe.Color + i + ".jpg");

            store.ShoesIds.Remove(shoeId);
            category.ShoesId.Remove(shoeId);



            await _unitOfWork.WriteCategoryRepository.UpdateAsync(category.Id);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            var result =  await _unitOfWork.WriteShoesRepository.RemoveAsync(shoeId);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> UpdateShoeCount(UpdateShoeCountDto dto)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(dto.ShoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            shoe.ShoeCountSizes.ForEach(x =>
            {
                foreach (var item in dto.ShoeCountSizes)
                {
                    if (x.Size == item.Size)
                    {
                        x.Count += item.Count;
                    }
                    else
                        shoe.ShoeCountSizes.Add(item);
                }
            });
            
            var result = await _unitOfWork.WriteShoesRepository.UpdateAsync(shoe.Id);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<List<GetShoeDto>> GetAllShoes(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");

            var shoesDto = new List<GetShoeDto>();
            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(x=> x.StoreId == storeId).ToList();
            foreach (var  shoe in shoes)
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
                CategoryId = shoe.CategoryId,
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
                Id = store.Id,
                Description = store.Description,
                Email = store.Email,
                Name = store.Name,
            };

            return storeDto;
        }
        #endregion


        #region Category

        public async Task<bool> CreateCategory(CreateCategoryDto dto)
        {
            var testCategory = await _unitOfWork.ReadCategoryRepository.GetAsync(x => x.Name.ToLower() == dto.Name.ToLower());
            if (testCategory is not null)
                throw new ArgumentException("Wrong Name");
             
            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                ShoesId = new List<string>(),
                StoreId = dto.StoreId
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

            foreach (var shoeId in category.ShoesId)
            {
                await RemoveShoe(shoeId);
            }

            var result = await _unitOfWork.WriteCategoryRepository.RemoveAsync(categoryId);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();
            return result;
        }
        
        public async Task<bool> UpdateCategory(UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(dto.Id);
            if (category is null)
                throw new ArgumentNullException();

            category.Name = dto.Name;

            var result = await _unitOfWork.WriteCategoryRepository.UpdateAsync(category.Id);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();
            return result;
        }

        public async Task<GetCategoryDto> GetCategory(string categoryId)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(categoryId);
            if (category is null)
                throw new ArgumentNullException("Wrong Category Id");

            var categoryDto = new GetCategoryDto
            {
                Id = categoryId,
                Name = category.Name,
                ShoesIds = category.ShoesId
            };

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
                    categoriesDto.Add(new GetCategoryDto
                    {
                        Id = category.Id,
                        Name = category.Name,
                        ShoesIds = category.ShoesId
                    });
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
                var result = await _unitOfWork.WriteOrderRepository.UpdateAsync(order.Id);
                await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
                return result;
            }
            
            order.OrderStatus = OrderStatus.Rejected;
            await _unitOfWork.WriteOrderRepository.UpdateAsync(order.Id);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
            return true;
        }


        public async Task<GetOrderDto> GetOrder(string orderId)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order is null)
                throw new ArgumentNullException();

            var orderDto = new GetOrderDto { 
                Id = orderId,
                StoreId = order.StoreId,
                CourierId = order.CourierId,
                OrderCommentId = order.OrderCommentId,
                Amount = order.Amount,
                OrderFinishTime = order.OrderFinishTime,
                OrderMakeTime = order.OrderMakeTime,
                OrderStatus = order.OrderStatus,
            };
            orderDto.ShoesIds.AddRange(order.ShoesIds);

            return orderDto;
        }


        public async Task<List<GetOrderDto>> GetAllOrder(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException();

            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x=> store.OrderIds.Contains(x.Id));

            var ordersDto = new List<GetOrderDto>();
            foreach (var order in orders)
            {
                if (order is not null)
                {
                    var orderDto = new GetOrderDto
                    {
                        Id = order.Id,
                        StoreId = order.StoreId,
                        CourierId = order.CourierId,
                        ShoesIds = order.ShoesIds,
                        OrderCommentId = order.OrderCommentId,
                        Amount = order.Amount,
                        OrderFinishTime = order.OrderFinishTime,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                    };
                    orderDto.ShoesIds.AddRange(order.ShoesIds);

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
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x => store.OrderIds.Contains(x.Id) && x.OrderStatus != OrderStatus.Rated);
            var ordersDto = new List<GetOrderDto>();
            foreach (var order in orders)
            {
                if (order is not null)
                    ordersDto.Add(new GetOrderDto
                    {
                        Id = order.Id,
                        StoreId = order.StoreId,
                        CourierId = order.CourierId,
                        ShoesIds = order.ShoesIds,
                        OrderCommentId = order.OrderCommentId,
                        Amount = order.Amount,
                        OrderFinishTime = default,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                    });
            }
            return ordersDto;
        }


        public async Task<bool> UpdateOrderStatus(UpdateOrderStatusDto orderDto)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderDto.OrderId);
            if (order is null)
                throw new ArgumentNullException();

            order.OrderStatus = orderDto.OrderStatus;

            var result = await _unitOfWork.WriteOrderRepository.UpdateAsync(order.Id);
            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
            return result;
        }


        public async Task<bool> RemoveOrder(string orderId)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order is null)
                throw new ArgumentNullException();

            var store = await _unitOfWork.ReadStoreRepository.GetAsync(order.StoreId);
            if (store is null)
                throw new ArgumentNullException();

            var client = await _unitOfWork.ReadClientRepository.GetAsync(order.ClientId);
            if (client is null)
                throw new ArgumentNullException();

            var courier = await _unitOfWork.ReadCourierRepository.GetAsync(order.CourierId);
            if (courier is null)
                throw new ArgumentNullException();


            store.OrderIds.Remove(orderId);
            client.OrdersId.Remove(orderId);
            courier.OrderIds.Remove(orderId);
            
            await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            await _unitOfWork.WriteClientRepository.UpdateAsync(client.Id);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            await _unitOfWork.WriteCourierRepository.UpdateAsync(courier.Id);
            await _unitOfWork.WriteCourierRepository.SaveChangesAsync();

            await _unitOfWork.WriteOrderCommentRepository.RemoveAsync(order.OrderCommentId);
            await _unitOfWork.WriteOrderCommentRepository.SaveChangesAsync();

            var result = await _unitOfWork.WriteOrderRepository.RemoveAsync(orderId);
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
                Id= orderComment.Id,
                ClientId = orderComment.ClientId,
                Content = orderComment.Content,
                CourierId= orderComment.CourierId,
                OrderId= orderComment.OrderId,
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
                        ClientId = orderComment.Id,
                        Content = orderComment.Content,
                        CourierId = orderComment.CourierId,
                        Id= orderComment.OrderId,
                        Rate = orderComment.Rate,
                        OrderId = orderComment.OrderId,
                    });
            }
            return orderCommentDto;
        }


        public async Task<bool> RemoveOrderComment(string orderCommentId)
        {
            var orderComment = await _unitOfWork.ReadOrderCommentRepository.GetAsync(orderCommentId);
            if (orderComment is null)
                throw new ArgumentNullException("");

            var result = await _unitOfWork.WriteOrderCommentRepository.RemoveAsync(orderCommentId);
            await _unitOfWork.WriteOrderCommentRepository.SaveChangesAsync();
            return result;
        }


        #endregion


        #region ShoeSalesStatistics

        public List<GeneralShoeStatisticsDto> WeeklySalesStatistics(string storeId)
        {
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x=> x.StoreId == storeId && x.OrderMakeTime > DateTime.Now.AddDays(-7).Date).ToList();
            if (orders.Count is 0)
                throw new ArgumentNullException("Order not Found");

            var shoesDto = new List<GeneralShoeStatisticsDto>();
            var ShoesIds = new List<string>();
            foreach (var order in orders)
                if (order is not null)
                    foreach (var shoeId in order.ShoesIds)
                        ShoesIds.Add(shoeId.ShoeId);

            var shoes = _unitOfWork.ReadShoesRepository.GetAll(); 
            foreach (var item in shoes)
            {
                if (item is not null)
                {
                    var shoe = ShoesIds.Where(x => x == item.Id).ToList();
                    if (shoe.Count != 0)
                        shoesDto.Add(new GeneralShoeStatisticsDto
                        {
                            ShoeId = item.Id,
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


        public async Task<List<DetailsShoeStatisticsDto>> DetailsShoeStatistics(string shoeId)
        {
            //var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            //if (shoe is null)
            //    throw new ArgumentNullException();

            //var orders  = _unitOfWork.ReadOrderRepository.GetWhere(order => order.ShoesIds.Contains(shoe.ShoeCountSizes));
            //if (orders.Count() == 0)
            //    throw new ArgumentNullException();


            //foreach (var order in orders)
            //{
            //    if (order is not null)
            //    {


            //    }
            //}

            throw new NotImplementedException();
        }


        #endregion
    }
}
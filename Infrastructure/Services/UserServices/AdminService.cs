using Application.IRepositories;
using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.OrderCommentDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using Domain.Models.Enum;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace Infrastructure.Services.UserServices
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Store

        public async Task<bool> CreateStore(AddStoreDto dto)
        {
            var testStore = _unitOfWork.ReadStoreRepository.GetAll().ToList();
            if (testStore.Count != 0)
                throw new ArgumentException("Store is found");

            var newStore = new Store
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Description = dto.Description,
                Email = dto.Email,
                OrderIds = new List<string>(),
                ShoesIds = new List<string>()
            };

            var result = await _unitOfWork.WriteStoreRepository.AddAsync(newStore); 
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UptadeStore(UpdateStoreDto dto)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(dto.Id);
            if (store is null)
                throw new ArgumentNullException("Wrong Store");

            store.Name = dto.Name;
            store.Description = dto.Description;
            store.Email = dto.Email;

            var result = await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();
            return result;
        }
        
        public async Task<GetStoreProfileDto> GetStore(string storeId)
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

        public async Task<bool> RemoveStore(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Wrong Store");

        
            foreach (var categoryId in store.CategoryIds)
            {
                await RemoveCategory(categoryId);   
            }

            foreach (var orderId in store.OrderIds)
            {
                // remove Order
            }

            await _unitOfWork.WriteOrderRepository.SaveChangesAsync();

            await _unitOfWork.WriteStoreRepository.RemoveAsync(storeId);
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

            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                ShoesId = new List<string>(),
                StoreId = dto.StoreId
            };

            await _unitOfWork.WriteStoreRepository.UpdateAsync(dto.StoreId);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            var result = await _unitOfWork.WriteCategoryRepository.AddAsync(newCategory);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            return result;
        }


        public async Task<bool> RemoveCategory(string categoryId)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(categoryId);
            if (category is null)
                throw new ArgumentNullException("Wrong Category Id");


            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(x => x.CategoryId == categoryId).ToList();

            foreach (var shoe in shoes)
            {
                // remove shoe
            }


            await _unitOfWork.WriteStoreRepository.RemoveAsync(category.StoreId);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            var result = await _unitOfWork.WriteCategoryRepository.RemoveAsync(categoryId);
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
                        ShoesIds= category.ShoesId
                    });
                }
            }
            return categoriesDto;
        }

        public async Task<bool> UpdateCategory(UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(dto.Id);
            if (category is null)
                throw new ArgumentNullException("Wrong Category Id");

            category.Name = dto.Name;

            var result = await _unitOfWork.WriteCategoryRepository.UpdateAsync(category.Id);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            return result;
        }

        #endregion


        #region Order


        public async Task<GetOrderDto> GetOrder(string orderId)
        {
            var order = await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order is null)
                throw new ArgumentNullException();

            var orderDto = new GetOrderDto
            {
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

            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x => store.OrderIds.Contains(x.Id));

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
        
        
        #region Shoe Comment


        public async Task<List<GetShoeCommentDto>> GetAllShoeComment(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException();

            var commentsDto = new List<GetShoeCommentDto>();

            foreach (var commentId in shoe.ShoeCommentId)
            {
                var comment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(commentId);
                if (comment is not null)
                    commentsDto.Add(new GetShoeCommentDto
                    {
                        Id = comment.Id,
                        ClientId = comment.ClientId,
                        ShoesId = comment.ShoesId,
                        Content = comment.Content,
                        OrderId = comment.OrderId,
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
                Id = comment.Id,
                ClientId = comment.ClientId,
                ShoesId = comment.ShoesId,
                Content = comment.Content,
                OrderId = comment.OrderId,
                Rate = comment.Rate,
            };
            return commentDto;
        }

        public async Task<bool> RemoveShoeComment(string commentId)
        {
            var comment = await _unitOfWork.ReadShoesCommentRepository.GetAsync(commentId);
            if (comment is null)
                throw new ArgumentNullException();

            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(comment.ShoesId);
            if (shoe is null)
                throw new ArgumentNullException();

            var client = await _unitOfWork.ReadClientRepository.GetAsync(comment.ClientId);
            if (client is null)
                throw new ArgumentNullException();

            shoe.ShoeCommentId.Remove(commentId);
            client.ShoesCommnetId.Remove(commentId);


            await _unitOfWork.WriteClientRepository.UpdateAsync(client.Id);
            await _unitOfWork.WriteClientRepository.SaveChangesAsync();

            await _unitOfWork.WriteShoesRepository.UpdateAsync(shoe.Id);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            var result = await _unitOfWork.WriteShoesCommentRepository.RemoveAsync(commentId);
            await _unitOfWork.WriteShoesCommentRepository.SaveChangesAsync();

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
                Id = orderComment.Id,
                ClientId = orderComment.ClientId,
                Content = orderComment.Content,
                CourierId = orderComment.CourierId,
                OrderId = orderComment.OrderId,
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
                        Id = orderComment.OrderId,
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
    }
}

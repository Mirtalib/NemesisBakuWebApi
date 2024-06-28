using Application.IRepositories;
using Application.Models.DTOs.CourierDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Services.IUserServices;

namespace Infrastructure.Services.UserServices
{
    public class CourierService : ICourierService
    {


        private readonly IUnitOfWork _unitOfWork;

        public CourierService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Order

        public List<GetOrderDto> GetAllOrder(string courierId)
        {
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(order => order.CourierId == courierId);
            if (orders.Count() == 0)
                throw new ArgumentNullException("You do not have an Order");
            

            var orderDto = new List<GetOrderDto>();

            foreach (var order in orders)
            {
                if (order is not null)
                    orderDto.Add(new GetOrderDto
                    {
                        Id = order.Id,
                        CourierId = courierId,
                        
                        StoreId = order.StoreId,
                        Amount = order.Amount,
                        OrderCommentId = order.OrderCommentId,
                        OrderFinishTime = order.OrderFinishTime,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                    });
            }
            return orderDto;
        }


        public async Task<GetOrderDto> GetOrder(string orderId)
        {
            var order =await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order == null)
                throw new ArgumentNullException("");

            var orderDto = new GetOrderDto
            {
                Id = orderId,
                StoreId= order.StoreId,
                CourierId = order.CourierId,
                ClientId = order.ClientId,
                Amount = order.Amount,
                OrderCommentId = order.OrderCommentId,
                OrderFinishTime = order.OrderFinishTime,
                OrderMakeTime = order.OrderMakeTime,
                OrderStatus = order.OrderStatus,
            };

            orderDto.ShoesIds.AddRange(order.ShoesIds);

            return orderDto;

        }


        #endregion

    }
}

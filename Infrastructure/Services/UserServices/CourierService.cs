using Application.IRepositories;
using Application.Models.DTOs.CourierDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Services.IUserServices;
using Domain.Models;

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
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(order => order.Courier.Id == courierId);
            if (orders.Count() == 0)
                throw new ArgumentNullException("You do not have an Order");
            

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
                        Amount = order.Amount,
                        OrderCommentId = order.OrderComment.Id,
                        OrderFinishTime = order.OrderFinishTime,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                    };
                    orderDto.ShoesIds.AddRange(order.Shoes);
                    ordersDto.Add(orderDto);
                }
            }
            return ordersDto;
        }


        public async Task<GetOrderDto> GetOrder(string orderId)
        {
            var order =await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order == null)
                throw new ArgumentNullException("");

            var orderDto = new GetOrderDto
            {
                Id = orderId,
                StoreId= order.Store.Id,
                CourierId = order.Courier.Id,
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


        #region Profile 

        public async Task<GetCourierProfileDto> GetProfile(string courierId)
        {
            var courier = await _unitOfWork.ReadCourierRepository.GetAsync(courierId);
            if (courier is null)
                throw new ArgumentNullException();

            var courierDto = new GetCourierProfileDto
            {
                Id= courierId,
                BrithDate = courier.BrithDate,
                Email = courier.Email,
                Surname = courier.Surname,
                Name = courier.Name,
                OrderSize = courier.Orders.Count(),
                PhoneNumber = courier.PhoneNumber,
                Rate = courier.Rate,
            };

            return courierDto;
        }
        #endregion
    }
}

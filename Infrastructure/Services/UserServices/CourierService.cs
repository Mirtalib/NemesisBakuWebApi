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
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(order => order.Courier.Id.ToString() == courierId);
            if (orders.Count() == 0)
                throw new ArgumentNullException("You do not have an Order");
            

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
                        Amount = order.Amount,
                        OrderCommentId = order.OrderComment.Id.ToString(),
                        OrderFinishTime = order.OrderFinishTime,
                        OrderMakeTime = order.OrderMakeTime,
                        OrderStatus = order.OrderStatus,
                        ShoesIds = new List<string>(),
                        ClientId = order.ClientId.ToString(),
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
            var order =await _unitOfWork.ReadOrderRepository.GetAsync(orderId);
            if (order == null)
                throw new ArgumentNullException("Order not found");

            var orderDto = new GetOrderDto
            {
                Id = orderId,
                StoreId= order.Store.Id.ToString(),
                CourierId = order.Courier.Id.ToString(),
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


        #region Profile 

        public async Task<GetCourierProfileDto> GetProfile(string courierId)
        {
            var courier = await _unitOfWork.ReadCourierRepository.GetAsync(courierId);
            if (courier is null)
                throw new ArgumentNullException("Profile not found");

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

        public async Task<bool> RemoveProfile(string courierId)
        {
            var courier = await _unitOfWork.ReadCourierRepository.GetAsync(courierId);
            if (courier is null)
                throw new ArgumentNullException("");

            foreach (var order in courier.Orders)
            {
                order.Courier = new();
                _unitOfWork.WriteOrderRepository.Update(order);
                await _unitOfWork.WriteOrderRepository.SaveChangesAsync();
            }

            var result = _unitOfWork.WriteCourierRepository.Remove(courier);
            await _unitOfWork.WriteCourierRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfile(UpdateCourierProfileDto dto)
        {
            var courier = await _unitOfWork.ReadCourierRepository.GetAsync(dto.Id);
            if (courier is null)
                throw new ArgumentNullException("");


            courier.Name = dto.Name;
            courier.Surname = dto.Surname;
            courier.PhoneNumber = dto.PhoneNumber;
            courier.BrithDate = dto.BrithDate;
            courier.Email = dto.Email;


            var result = _unitOfWork.WriteCourierRepository.Update(courier);
            await _unitOfWork.WriteCourierRepository.SaveChangesAsync();

            return result;
        }



        #endregion
    }
}

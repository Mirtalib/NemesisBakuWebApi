using Application.Models.DTOs.OderDTOs;

namespace Application.Services.IUserServices
{
    public interface ICourierService
    {
        #region Order

        List<GetOrderDto> GetAllOrder(string courierId);

        Task<GetOrderDto> GetOrder(string orderId);


        #endregion



    }
}

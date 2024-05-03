using Application.Models.DTOs.OderDTOs;

namespace Application.Services.IUserServices
{
    public interface IClientService
    {

        Task<bool> MakeOrder(MakeOrderDto dto);
    }
}

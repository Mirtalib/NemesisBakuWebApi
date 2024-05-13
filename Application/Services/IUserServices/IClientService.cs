using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesDTOs;

namespace Application.Services.IUserServices
{
    public interface IClientService
    {

        #region Shoe 
        
        Task<List<GetShoeDto>> GetAllShoes(string storeId);

        Task<GetShoeInfoDto> GetShoeId(string shoeId);

        Task<List<GetShoeDto>> GetShoeByCategoryId(string categoryId);
        
        #endregion


        #region Favori List
        
        Task<List<GetShoeDto>> GetFavoriteList(string clientId);

        Task<bool> AddToShoeFavoriteList(AddFavoriteListDto dto);

        Task<bool> RemoveToShoeFavoriteList(RemoveFavoriteListDto dto);

        #endregion


        #region Order

        Task<bool> MakeOrder(MakeOrderDto dto);

        List<GetOrderDto> GetAllOrder(string clientId);

        #endregion

    }
}

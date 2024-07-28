using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
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

        Task<bool> RemoveToShoeFavoriteList(string favoriShoeId);

        #endregion


        #region Order

        Task<bool> MakeOrder(MakeOrderDto dto);

        List<GetOrderDto> GetAllOrder(string clientId);

        Task<GetOrderDto> GetOrder(string orderId);
        #endregion


        #region Shopping List

        Task<List<GetShoeDto>> GetShoppingList(string clientId);

        Task<bool> AddToShoeShoppingList(AddShoppingListDto dto);

        Task<bool> RemoveToShoeShoppingList(string shoppingShoeId);
        #endregion


        #region Profile

        Task<GetClientProfileDto> GetProfile(string clientId);

        Task<bool> RemoveProfile(string clientId);

        Task<bool> UpdateProfile(UpdateClientProfileDto dto);

        #endregion


        #region Shoe Comment

        public Task<GetShoeCommentDto> GetShoeComment(string commentId);

        public List<GetShoeCommentDto> GetAllShoeComment(string clientId);

        public Task<bool> CreateShoeComment(CreateShoeCommentDto dto);

        public Task<bool> RemoveShoeComment(string shoeCommentId);
        #endregion


    }
}

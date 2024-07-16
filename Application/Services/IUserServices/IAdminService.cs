using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.OrderCommentDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Models.DTOs.StoreDTOs;

namespace Application.Services.IUserServices
{
    public interface IAdminService
    {


        #region Shoe

        Task<bool> CreateShoe(AddShoeDto shoeDto);

        Task<bool> CreateShoeImages(AddShoeImageDto dto);

        Task<bool> UpdateShoeImage(UpdateShoeImageDto dto);

        Task<GetShoeInfoDto> GetShoeId(string shoeId);

        Task<List<GetShoeDto>> GetAllShoes(string storeId);

        Task<bool> UpdateShoeCount(UpdateShoeCountDto dto);

        Task<bool> RemoveShoe(string shoeId);

        Task<bool> UpdateShoe(UpdateShoeDto dto);

        #endregion


        #region Store
        Task<bool> CreateStore(AddStoreDto dto);

        Task<bool> UptadeStore(UpdateStoreDto dto);

        //Task<bool> UptadeStorePassword(UptadeRestaurantPasswordDto dto);
        Task<bool> RemoveStore(string storeId);
        Task<GetStoreProfileDto> GetStore(string storeId);

        #endregion


        #region Category

        Task<bool> CreateCategory(CreateCategoryDto dto);

        Task<bool> RemoveCategory(string categoryId);

        Task<bool> UpdateCategory(UpdateCategoryDto dto);

        Task<GetCategoryDto> GetCategory(string categoryId);

        List<GetCategoryDto> GetAllCategory();

        #endregion


        #region Order

        Task<GetOrderDto> GetOrder(string orderId);

        Task<List<GetOrderDto>> GetAllOrder(string storeId);

        Task<bool> RemoveOrder(string orderId);

        #endregion


        #region ShoeComment

        Task<List<GetShoeCommentDto>> GetAllShoeComment(string shoeId);

        Task<GetShoeCommentDto> GetShoeComment(string commentId);

        Task<bool> RemoveShoeComment(string commentId);


        #endregion


        #region Order Comment

        Task<GetOrderCommentDto> GetOrderComment(string orderCommentId);

        List<GetOrderCommentDto> GetAllOrderComment();

        Task<bool> RemoveOrderComment(string orderCommentId);

        #endregion

    }
}

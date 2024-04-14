using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.StoreDTOs;

namespace Application.Services.IUserServices
{
    public interface IAdminService
    {
        #region Store
        Task<bool> CreateStore(AddStoreDto dto);

        //Task<bool> UptadeStore(UpdateRestaurantDto dto);

        //Task<bool> UptadeStorePassword(UptadeRestaurantPasswordDto dto);

        //Task<bool> RemoveStore(string restaurantId);
        Task<GetStoreProfileDto> GetStore(string storeId);

        #endregion


        #region Category

        Task<bool> CreateCategory(CreateCategoryDto dto);

        #endregion

    }
}

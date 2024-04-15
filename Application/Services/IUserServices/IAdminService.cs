using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.StoreDTOs;

namespace Application.Services.IUserServices
{
    public interface IAdminService
    {


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

    }
}

using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Models.DTOs.StoreDTOs;

namespace Application.Services.IUserServices
{
    public interface IStoreService
    {
        #region Shoe
        Task<List<GetShoeDto>> GetAllShoes(string storeId);
        Task<GetShoeInfoDto> GetShoeId(string shoeId);
        Task<bool> UpdateShoeCount (UpdateShoeCountDto dto);
        Task<bool> CreateShoe(AddShoeDto shoe);
        Task<bool> RemoveShoe(string shoeId);
        #endregion

        #region Profile
        Task<GetStoreProfileDto> GetProfile(string storeId);


        #endregion

        List<GeneralShoeStatistics> WeeklySalesStatistics(string storeId);


        #region Category

        Task<bool> CreateCategory(CreateCategoryDto dto);

        Task<bool> RemoveCategory(string categoryId);

        Task<GetCategoryDto> GetCategory(string categoryId);

        List<GetCategoryDto> GetAllCategory();

        #endregion
    }
}

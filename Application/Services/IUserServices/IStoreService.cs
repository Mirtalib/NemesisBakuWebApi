using Application.IRepositories;
using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Models.DTOs.StoreDTOs;

namespace Application.Services.IUserServices
{
    public interface IStoreService
    {
        #region Shoe
        Task<List<GetShoeDto>> GetAllShoes(string storeId);
        Task<bool> AddShoeImages(AddShoeImageDto dto);
        Task<GetShoeInfoDto> GetShoeId(string shoeId);
        Task<bool> UpdateShoeCount (UpdateShoeCountDto dto);
        Task<bool> CreateShoe(AddShoeDto shoe);
        Task<bool> RemoveShoe(string shoeId);
        #endregion

        #region Profile
        Task<GetStoreProfileDto> GetProfile(string storeId);


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

        Task<List<GetOrderDto>> GetActiveOrder(string storeId);

        Task<bool> InLastDecidesSituation(InLastSituationOrderDto orderDto);

        Task<bool> UpdateOrderStatus(UpdateOrderStatusDto orderDto);
        #endregion

        List<GeneralShoeStatisticsDto> WeeklySalesStatistics(string storeId);

    }
}

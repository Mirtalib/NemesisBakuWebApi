using Application.Models.DTOs.ShoesDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.IUserServices
{
    public interface IStoreService
    {
        #region Shoe
        Task<List<GetShoeDto>> GetAllShoes(string storeId);
        Task<GetShoeInfoDto> GetShoeId(string shoeId);
        Task<bool> CreateShoe(AddShoeDto shoe);
        Task<bool> RemoveShoe(string shoeId);
        #endregion
    }
}

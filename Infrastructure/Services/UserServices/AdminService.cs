using Application.IRepositories;
using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IUserServices;
using Domain.Models;

namespace Infrastructure.Services.UserServices
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCategory(CreateCategoryDto dto)
        {
            var testCategory = await _unitOfWork.ReadCategoryRepository.GetAsync(x => x.Name.ToLower() == dto.Name.ToLower());
            if (testCategory is not null)
                throw new ArgumentException("Wrong Name");

            var newCategory = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                ShoesId = new List<string>()
            };

            var result = await _unitOfWork.WriteCategoryRepository.AddAsync(newCategory);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            return result;
        }

        public async Task<bool> CreateStore(AddStoreDto dto)
        {
            var testStore = _unitOfWork.ReadStoreRepository.GetAll().ToList();
            if (testStore.Count != 0)
                throw new ArgumentException("Store is found");

            var newStore = new Store
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Description = dto.Description,
                Email = dto.Email,
                OrderId = new List<string>(),
                ShoesId = new List<string>()
            };

            var result = await _unitOfWork.WriteStoreRepository.AddAsync(newStore); 
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();
            return result;
        }

        public async Task<GetStoreProfileDto> GetStore(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");
            var storeDto = new GetStoreProfileDto
            {
                Id = store.Id,
                Description = store.Description,
                Email = store.Email,
                Name = store.Name,
            };

            return storeDto;
        }
    }
}

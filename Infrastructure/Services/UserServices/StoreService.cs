﻿using Application.IRepositories;
using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IHelperServices;
using Application.Services.IUserServices;
using Domain.Models;
using FluentValidation;

namespace Infrastructure.Services.UserServices
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBlobService _blobSerice;
        private readonly IValidator<AddShoeDto> _addShoeValidator;

        public StoreService(IUnitOfWork unitOfWork, IBlobService blobSerice, IValidator<AddShoeDto> addShoeValidator)
        {
            _unitOfWork = unitOfWork;
            _blobSerice = blobSerice;
            _addShoeValidator = addShoeValidator;
        }

        #region Shoe

        public async Task<bool> CreateShoe(AddShoeDto shoeDto)
        {
            var isValid  = _addShoeValidator.Validate(shoeDto);
            if (isValid.IsValid)
            {
                var store = await _unitOfWork.ReadStoreRepository.GetAsync(shoeDto.StoreId);
                if (store is null)
                    throw new ArgumentNullException("Store is not found");

                var category = await _unitOfWork.ReadCategoryRepository.GetAsync(shoeDto.CategoryId);
                if (category is null)
                    throw new ArgumentNullException("Category is not found");

                var newShoe = new Shoe
                {
                    Id = Guid.NewGuid().ToString(),
                    Brend = shoeDto.Brend,
                    Model = shoeDto.Model,
                    Price = shoeDto.Price,
                    CategoryId = shoeDto.CategoryId,
                    Color = shoeDto.Color,
                    Description = shoeDto.Description,
                    ShoeCountSize = shoeDto.ShoeCountSize
                };

                for (int i = 0; i < shoeDto.Images.Length; i++)
                {
                    var form = shoeDto.Images[i];
                    using (var stream = form.OpenReadStream())
                    {
                        var fileName = newShoe.Id + "-" + newShoe.Model + newShoe.Color + i + ".jpg";
                        var contentType = form.ContentType;

                        var blobResult = await _blobSerice.UploadFileAsync(stream, fileName, contentType);
                        if (blobResult is false)
                            return false;

                        newShoe.ImageUrls.Add(_blobSerice.GetSignedUrl(fileName));
                    }
                }

                store.ShoesId.Add(newShoe.Id);
                category.ShoesId.Add(newShoe.Id);

                await _unitOfWork.WriteShoesRepository.AddAsync(newShoe);
                await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

                await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
                await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

                await _unitOfWork.WriteCategoryRepository.UpdateAsync(category.Id);
                await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

                return true;
            }
            throw new ValidationException("No Valid");
        }

        public async Task<bool> RemoveShoe(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            var store = await _unitOfWork.ReadStoreRepository.GetAsync(shoe.StoreId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");

            var category = await _unitOfWork.ReadCategoryRepository.GetAsync(shoe.CategoryId);
            if (category is null)
                throw new ArgumentNullException("Category is not found");



            for (int i = 0; i < shoe.ImageUrls.Count; i++)
                await _blobSerice.DeleteFileAsync(shoe.Id + "-" + shoe.Model + shoe.Color + i + ".jpg");

            store.ShoesId.Remove(shoeId);
            category.ShoesId.Remove(shoeId);


            await _unitOfWork.WriteStoreRepository.UpdateAsync(store.Id);
            await _unitOfWork.WriteStoreRepository.SaveChangesAsync();

            await _unitOfWork.WriteCategoryRepository.UpdateAsync(category.Id);
            await _unitOfWork.WriteCategoryRepository.SaveChangesAsync();

            var result =  await _unitOfWork.WriteShoesRepository.RemoveAsync(shoeId);
            await _unitOfWork.WriteShoesRepository.SaveChangesAsync();

            return result;
        }

        public async Task<List<GetShoeDto>> GetAllShoes(string storeId)
        {
            var store = await _unitOfWork.ReadStoreRepository.GetAsync(storeId);
            if (store is null)
                throw new ArgumentNullException("Store is not found");

            var shoesDto = new List<GetShoeDto>();
            var shoes = _unitOfWork.ReadShoesRepository.GetWhere(x=> x.StoreId == storeId).ToList();
            foreach (var shoe in shoes)
            {
                if (shoe is not null)
                    shoesDto.Add(new GetShoeDto
                    {
                        Id = shoe.Id,
                        Brend = shoe.Brend,
                        ImageUrls = shoe.ImageUrls,
                        Model = shoe.Model,
                        Price = shoe.Price,
                    });
            }
            return shoesDto;
        }

        public async Task<GetShoeInfoDto> GetShoeId(string shoeId)
        {
            var shoe = await _unitOfWork.ReadShoesRepository.GetAsync(shoeId);
            if (shoe is null)
                throw new ArgumentNullException("Shoe not found");

            var shoeDto = new GetShoeInfoDto
            {
                Id = shoe.Id,
                Brend = shoe.Brend,
                ImageUrls = shoe.ImageUrls,
                Model = shoe.Model,
                Price = shoe.Price,
                CategoryId = shoe.CategoryId,
                Color = shoe.Color,
                Description = shoe.Description,
                ShoeCountSize = shoe.ShoeCountSize,
            };

            return shoeDto;
        }

        #endregion

        #region Category

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
        #endregion

        #region Profile
        public async Task<GetStoreProfileDto> GetProfile(string storeId)
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
        #endregion


        #region ShoeSalesStatistics
        public List<GeneralShoeStatistics> WeeklySalesStatistics(string storeId)
        {
            var orders = _unitOfWork.ReadOrderRepository.GetWhere(x=> x.StoreId == storeId && x.OrderDate > DateTime.Now.AddDays(-7).Date).ToList();
            if (orders.Count is 0)
                throw new ArgumentNullException("Order not Found");

            var shoesDto = new List<GeneralShoeStatistics>();
            var ShoesIds = new List<string>();
            foreach (var order in orders)
                if (order is not null)
                    foreach (var shoeId in order.ShoesIds)
                        ShoesIds.Add(shoeId);

            var shoes = _unitOfWork.ReadShoesRepository.GetAll(); 
            foreach (var item in shoes)
            {
                if (item is not null)
                {
                    var shoe = ShoesIds.Where(x => x == item.Id).ToList();
                    if (shoe.Count != 0)
                        shoesDto.Add(new GeneralShoeStatistics
                        {
                            ShoeId = item.Id,
                            Brend = item.Brend,
                            Model = item.Model,
                            Size = Convert.ToByte(shoe.Count),
                        });
                }
            }
            return shoesDto;
        }
        #endregion
    }
}

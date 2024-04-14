using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IUserServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {


        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpPost("createStore")]
        public async Task<ActionResult<bool>> CreateStore(AddStoreDto storeDto)
        {
            try
            {
                var result = await _adminService.CreateStore(storeDto);
                if (result)
                {
                    Log.Information("Create operation completed successfully on [Post] CreateStore");
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] AddStore Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getStore")]
        public async Task<ActionResult<GetStoreProfileDto>> GetStore(string storeId)
        {
            try
            {
                return Ok(await _adminService.GetStore(storeId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetStore Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        #region Category

        [HttpPost("createCategory")]
        public async Task<ActionResult<bool>> CreateCategory(CreateCategoryDto dto)
        {
            try
            {
                var result = await _adminService.CreateCategory(dto);
                if (result)
                {
                    Log.Information("Create operation completed successfully on [Post] CreateCategory");
                    return Ok(result);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] CreateCategory Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        #endregion
    }
}
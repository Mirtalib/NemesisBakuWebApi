using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using Infrastructure.Services.UserServices;
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


        #region Store
        
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


        [HttpPost("uptadeStore")]
        public async Task<ActionResult<bool>> UptadeStore(UpdateStoreDto dto)
        {
            try
            {
                return Ok(await _adminService.UptadeStore(dto));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UptadeStore Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeStore")]
        public async Task<ActionResult<bool>> RemoveStore(string storeId)
        {
            try
            {
                return Ok(await _adminService.GetStore(storeId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveStore Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion


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



        [HttpDelete("removeCategory")]
        public async Task<ActionResult<bool>> RemoveCategory(string categoryId)
        {
            try
            {
                // Log.Information("Create operation completed successfully on [Post] CreateCategory");
                return Ok(await _adminService.RemoveCategory(categoryId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveCategory Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("getCategory")]
        public async Task<ActionResult<GetCategoryDto>> GetCategory(string categoryId)
        {
            try
            {
                return Ok(await _adminService.GetCategory(categoryId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetCategory Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("getAllCategory")]
        public ActionResult<List<GetCategoryDto>> GetAllCategory()
        {
            try
            {
                return Ok( _adminService.GetAllCategory());
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllCategory Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("updateCategory")]
        public async Task<ActionResult<bool>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var result = await _adminService.UpdateCategory(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateCategory Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion


        #region Shoe Comment

        [HttpGet("GetAllShoeComment")]
        public async Task<ActionResult<List<GetShoeCommentDto>>> GetAllShoeComment(string shoeId)
        {
            try
            {
                var result = await _adminService.GetAllShoeComment(shoeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllShoeComment Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getShoeComment")]
        public async Task<ActionResult<GetShoeCommentDto>> GetShoeComment(string commentId)
        {
            try
            {
                var result = await _adminService.GetShoeComment(commentId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetShoeComment Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeShoeComment")]
        public async Task<ActionResult<bool>> RemoveShoeComment(string commentId)
        {
            try
            {
                var result = await _adminService.RemoveShoeComment(commentId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveShoeComment Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        #endregion
        
    }
}
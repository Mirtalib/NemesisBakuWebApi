using Application.Models.DTOs.AdminDTOs;
using Application.Models.DTOs.CategoryDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.OrderCommentDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Models.DTOs.StoreDTOs;
using Application.Services.IUserServices;
using Domain.Models;
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


        #region Shoe

        [HttpPost("createShoe")]
        public async Task<ActionResult<bool>> CreateShoe(AddShoeDto dto)
        {
            try
            {
                var result = await _adminService.CreateShoe(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] CreateShoe Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("createShoeImages")]
        public async Task<ActionResult<bool>> CreateShoeImages([FromForm] AddShoeImageDto dto)
        {
            try
            {
                var result = await _adminService.CreateShoeImages(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] CreateShoeImages Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("updateShoeImage")]
        public async Task<ActionResult<bool>> UpdateShoeImage(UpdateShoeImageDto dto)
        {
            try
            {
                var result = await _adminService.UpdateShoeImage(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateShoeImage Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("updateShoeCount")]
        public async Task<ActionResult<bool>> UpdateShoeCount(UpdateShoeCountDto dto)
        {
            try
            {
                var result = await _adminService.UpdateShoeCount(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateShoeCount Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("updateShoe")]
        public async Task<ActionResult<bool>> UpdateShoe(UpdateShoeDto dto)
        {
            try
            {
                var result = await _adminService.UpdateShoe(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateShoe Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getShoeId")]
        public async Task<ActionResult<GetShoeInfoDto>> GetShoeId(string shoeId)
        {
            try
            {
                var result = await _adminService.GetShoeId(shoeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetShoeId Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getAllShoes")]
        public async Task<ActionResult<List<GetShoeDto>>> GetAllShoes(string storeId)
        {
            try
            {
                var result = await _adminService.GetAllShoes(storeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllShoes Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeShoe")]
        public async Task<ActionResult<bool>> RemoveShoe(string shoeId)
        {
            try
            {
                var result = await _adminService.RemoveShoe(shoeId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveShoe Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }



        #endregion


        #region Store

        [HttpPost("createStore")]
        public async Task<ActionResult<bool>> CreateStore(AddStoreDto dto)
        {
            try
            {
                var result = await _adminService.CreateStore(dto);
                if (result)
                    return Ok(result);
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
                var result = await _adminService.GetStore(storeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _adminService.UptadeStore(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
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
                var result = await _adminService.RemoveStore(storeId);
                if (result)
                    return Ok(result);
                return BadRequest();
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
                    return Ok(result);
                
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
                var result = await _adminService.RemoveCategory(categoryId);
                if (result)
                    return Ok(result);
                return BadRequest();
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
                var result = await _adminService.GetCategory(categoryId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = _adminService.GetAllCategory();
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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


        #region Order


        [HttpGet("getOrder")]
        public async Task<ActionResult<GetOrderDto>> GetOrder(string orderId)
        {
            try
            {
                var result = await _adminService.GetOrder(orderId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetOrder Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getAllOrder")]
        public async Task<ActionResult<List<GetOrderDto>>> GetAllOrder(string storeId)
        {
            try
            {
                var result = await _adminService.GetAllOrder(storeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllOrder Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removeOrder")]
        public async Task<ActionResult<bool>> RemoveOrder(string orderId)
        {
            try
            {
                var result = await _adminService.RemoveOrder(orderId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveOrder Error ->{ex.Message}");
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


        #region Order Comment


        [HttpGet("getOrderComment")]
        public async Task<ActionResult<GetOrderCommentDto>> GetOrderComment(string orderCommentId)
        {
            try
            {
                var result = await _adminService.GetOrderComment(orderCommentId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetOrderComment Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("getAllOrderComment")]
        public ActionResult<List<GetOrderCommentDto>> GetAllOrderComment()
        {
            try
            {
                var result =  _adminService.GetAllOrderComment();
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllOrderComment Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("removeOrderComment")]
        public async Task<ActionResult<bool>> RemoveOrderComment(string orderCommentId)
        {
            try
            {
                var result =await _adminService.RemoveOrderComment(orderCommentId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveOrderComment Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        #endregion


        #region Profile

        [HttpGet("getProfile")]
        public async Task<ActionResult<GetAdminProfileDto>> GetProfile(string adminId)
        {
            try
            {
                var result = await _adminService.GetProfile(adminId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetProfile Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeProfile")]
        public async Task<ActionResult<bool>> RemoveProfile(string adminId)
        {
            try
            {
                var result = await _adminService.RemoveProfile(adminId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveProfile Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost("updateProfile")]
        public async Task<ActionResult<bool>> UpdateProfile(UpdateAdminProfileDto dto)
        {
            try
            {
                var result = await _adminService.UpdateProfile(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateProfile Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
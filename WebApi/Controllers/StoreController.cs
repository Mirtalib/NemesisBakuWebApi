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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


        #region Shoe
        [HttpPost("createShoe")]
        public async Task<ActionResult<bool>> CreateShoe(AddShoeDto dto)
        {
            try
            {
                var result = await _storeService.CreateShoe(dto);
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
                var result = await _storeService.CreateShoeImages(dto);
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
                var result = await _storeService.UpdateShoeImage(dto);
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
                var result = await _storeService.UpdateShoeCount(dto);
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
                var result = await _storeService.UpdateShoe(dto);
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
                var result = await _storeService.GetShoeId(shoeId);
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
                var result = await _storeService.GetAllShoes(storeId);
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
                var result = await _storeService.RemoveShoe(shoeId);
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


        #region Profile


        [HttpGet("getProfile")]
        public async Task<ActionResult<GetStoreProfileDto>> GetProfile(string storeId)
        {
            try
            {
                var result = await _storeService.GetProfile(storeId);
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


        [HttpPost("uptadeStore")]
        public async Task<ActionResult<bool>> UptadeStore(UpdateStoreDto dto)
        {
            try
            {
                var result = await _storeService.UptadeStore(dto);
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
                var result = await _storeService.RemoveStore(storeId);
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
                var result = await _storeService.CreateCategory(dto);
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


        [HttpGet("getCategory")]
        public async Task<ActionResult<GetCategoryDto>> GetCategory(string categoryId)
        {

            try
            {
                var result = await _storeService.GetCategory(categoryId);
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


        [HttpGet("GetAllCategory")]
        public ActionResult<List<GetCategoryDto>> GetAllCategory(string storeId)
        {
            try
            {
                var result = _storeService.GetAllCategory();
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
                var result = await _storeService.UpdateCategory(dto);
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


        [HttpDelete("updateCategory")]
        public async Task<ActionResult<bool>> RemoveCategory(string categoryId)
        {
            try
            {
                var result = await _storeService.RemoveCategory(categoryId);
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


        #endregion


        #region Order

        [HttpPost("inLastDecidesSituation")]
        public async Task<ActionResult<bool>> InLastDecidesSituation(InLastSituationOrderDto orderDto)
        {
            try
            {
                var result = await _storeService.InLastDecidesSituation(orderDto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] InLastDecidesSituation Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getOrder")]
        public async Task<ActionResult<GetOrderDto>> GetOrder(string orderId)
        {
            try
            {
                var result = await _storeService.GetOrder(orderId);
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
                var result = await _storeService.GetAllOrder(storeId);
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




        [HttpGet("getActiveOrder")]
        public async Task<ActionResult<List<GetOrderDto>>> GetActiveOrder(string storeId)
        {
            try
            {
                var result = await _storeService.GetActiveOrder(storeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetActiveOrder Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("UpdateOrderStatus")]
        public async Task<ActionResult<bool>> UpdateOrderStatus(UpdateOrderStatusDto orderDto)
        {
            try
            {
                var result = await _storeService.UpdateOrderStatus(orderDto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateOrderStatus Error ->{ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeOrder")]
        public async Task<ActionResult<bool>> RemoveOrder(string orderId)
        {
            try
            {
                var result = await _storeService.RemoveOrder(orderId);
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


        #region Order Comment


        [HttpGet("GetOrderComment")]
        public async Task<ActionResult<GetOrderCommentDto>> GetOrderComment(string orderCommentId)
        {
            try
            {
                var result = await _storeService.GetOrderComment(orderCommentId);
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


        [HttpGet("GetAllOrderComment")]
        public ActionResult<List<GetOrderCommentDto>> GetAllOrderComment(string storeId)
        {
            try
            {
                var result = _storeService.GetAllOrderComment();
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

        [HttpDelete("RemoveOrderComment")]
        public async Task<ActionResult<bool>> RemoveOrderComment(string orderCommentId)
        {
            try
            {
                var result = await _storeService.RemoveOrderComment(orderCommentId);
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


        #region Shoe Comment


        [HttpGet("getAllShoeComment")]
        public async Task<ActionResult<List<GetShoeCommentDto>>> GetAllShoeComment(string shoeId)
        {
            try
            {
                var result = await _storeService.GetAllShoeComment(shoeId);
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
                var result = await _storeService.GetShoeComment(commentId);
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
                var result = await _storeService.RemoveShoeComment(commentId);
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

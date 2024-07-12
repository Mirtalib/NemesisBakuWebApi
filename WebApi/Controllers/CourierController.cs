using Application.Models.DTOs.CourierDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierController : ControllerBase
    {

        private readonly ICourierService _courierService;

        public CourierController(ICourierService courierService)
        {
            _courierService = courierService;
        }


        #region Order

        [HttpGet("getAllOrder")]
        public ActionResult<List<GetOrderDto>> GetAllOrder(string courierId)
        {
            try
            {
                var result = _courierService.GetAllOrder(courierId);
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


        [HttpGet("getOrder")]
        public async Task<ActionResult<GetOrderDto>> GetOrder(string orderId)
        {
            try
            {
                var result =await _courierService.GetOrder(orderId);
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



        #endregion


        #region Profile 

        [HttpGet("getProfile")]
        public async Task<ActionResult<GetCourierProfileDto>> GetProfile(string courierId)
        {
            try
            {
                var result = await _courierService.GetProfile(courierId);
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

        #endregion
    }

}

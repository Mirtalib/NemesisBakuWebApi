using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        #region Shoe Comment

        [HttpPost("createShoeComment")]
        public async Task<ActionResult<bool>> CreateShoeComment(CreateShoeCommentDto dto)
        {
            try
            {
                var result = await _clientService.CreateShoeComment(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] CreateShoeComment Error -> {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removeShoeComment")]
        public async Task<ActionResult<bool>> RemoveShoeComment(string shoeCommentId)
        {
            try
            {
                var result = await _clientService.RemoveShoeComment(shoeCommentId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveShoeComment Error -> {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Favori List


        [HttpGet("getFavoriteList")]
        public async Task<ActionResult<List<GetShoeDto>>> GetFavoriteList(string clientId)
        {

            try
            {
                return Ok(await _clientService.GetFavoriteList(clientId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetFavoriteList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("addToShoeFavoriteList")]
        public async Task<ActionResult<bool>> AddToShoeFavoriteList(AddFavoriteListDto dto)
        {
            try
            {
                return Ok(await _clientService.AddToShoeFavoriteList(dto));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] AddToShoeFavoriteList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeToShoeFavoriteList")]
        public async Task<ActionResult<bool>> RemoveToShoeFavoriteList(RemoveFavoriteListDto dto)
        {
            try
            {
                return Ok(await _clientService.RemoveToShoeFavoriteList(dto));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveToShoeFavoriteList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        #endregion


        #region ShoppingList


        [HttpGet("getShoppingList")]
        public async Task<ActionResult<List<GetShoeDto>>> GetShoppingList(string clientId)
        {
            try
            {
                return Ok(await _clientService.GetShoppingList(clientId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetShoppingList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("addToShoeShoppingList")]
        public async Task<ActionResult<bool>> AddToShoeShoppingList(AddShoppingListDto dto)
        {
            try
            {
                return Ok(await _clientService.AddToShoeShoppingList(dto));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] AddToShoeShoppingList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeToShoeShoppingList")]
        public async Task<ActionResult<bool>> RemoveToShoeShoppingList(RemoveShoppingListDto dto)
        {
            try
            {
                return Ok(await _clientService.RemoveToShoeShoppingList(dto));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveToShoeShoppingList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion


        #region Profile

        [HttpGet("getProfile")]
        public async Task<ActionResult<GetClientProfileDto>> GetProfile(string clientId)
        {
            try
            {
                var result = await _clientService.GetProfile(clientId);
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


        [HttpPost("updateProfile")]
        public async Task<ActionResult<bool>> UpdateProfile(UpdateClientProfileDto dto)
        {
            try
            {
                var result = await _clientService.UpdateProfile(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] UpdateProfile Error -> {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeProfile")]
        public async Task<ActionResult<bool>> RemoveProfile(string clientId)
        {
            try
            {
                var result = await _clientService.RemoveProfile(clientId);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [DELETE] RemoveProfile Error -> {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion


        #region Shoe 

        [HttpGet("getAllShoes")]
        public async Task<ActionResult<List<GetShoeDto>>> GetAllShoes(string storeId)
        {
            try
            {
                return Ok(await _clientService.GetAllShoes(storeId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllShoes => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getShoeByCategoryId")]
        public async Task<ActionResult<List<GetShoeDto>>> GetShoeByCategoryId(string categoryId)
        {
            try
            {
                return Ok(await _clientService.GetShoeByCategoryId(categoryId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetShoeByCategoryId => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getShoeId")]
        public async Task<ActionResult<GetShoeInfoDto>> GetShoeId(string shoeId)
        {
            try
            {
                return Ok(await _clientService.GetShoeId(shoeId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetShoeByCategoryId => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        #endregion


        #region Order


        [HttpPost("makeOrder")]
        public async Task<ActionResult<bool>> MakeOrder(MakeOrderDto dto)
        {
            try
            {
                return Ok(await _clientService.MakeOrder(dto));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] MakeOrder => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getAllOrder")]
        public ActionResult<List<GetOrderDto>> GetAllOrder(string clientId)
        {
            try
            {
                return Ok(_clientService.GetAllOrder(clientId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllOrder => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getOrder")]
        public async Task<ActionResult<GetOrderDto>> GetOrder(string orderId)
        {
            try
            {
                return Ok(await _clientService.GetOrder(orderId));
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetOrder => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        #endregion

    }
}
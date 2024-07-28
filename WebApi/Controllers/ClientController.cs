using Application.Models.DTOs.ClientDTOs;
using Application.Models.DTOs.OderDTOs;
using Application.Models.DTOs.ShoesCommentDTOs;
using Application.Models.DTOs.ShoesDTOs;
using Application.Services.IUserServices;
using Domain.Models;
using Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.ComponentModel.Design;

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

        [HttpGet("getShoeComment")]
        public async Task<ActionResult<GetShoeCommentDto>> GetShoeComment(string commentId)
        {
            try
            {
                var result = await _clientService.GetShoeComment(commentId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetShoeComment Error -> {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getAllShoeComment")]
        public ActionResult<List<GetShoeCommentDto>> GetAllShoeComment(string clientId)
        {
            try
            {
                var result =  _clientService.GetAllShoeComment(clientId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [GET] GetAllShoeComment Error -> {ex.Message}");
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
                var result = await  _clientService.GetFavoriteList(clientId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.AddToShoeFavoriteList(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] AddToShoeFavoriteList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeToShoeFavoriteList")]
        public async Task<ActionResult<bool>> RemoveToShoeFavoriteList(string favoriShoeId)
        {
            try
            {
                var result = await _clientService.RemoveToShoeFavoriteList(favoriShoeId);
                if (result)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.GetShoppingList(clientId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.AddToShoeShoppingList(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
            }
            catch (Exception ex)
            {
                Log.Error($"Error occured on [POST] AddToShoeShoppingList => {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("removeToShoeShoppingList")]
        public async Task<ActionResult<bool>> RemoveToShoeShoppingList(string shoppingShoeId)
        {
            try
            {
                var result = await _clientService.RemoveToShoeShoppingList(shoppingShoeId);
                if (result)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.GetAllShoes(storeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.GetShoeByCategoryId(categoryId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.GetShoeId(shoeId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.MakeOrder(dto);
                if (result)
                    return Ok(result);
                return BadRequest();
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
                var result = _clientService.GetAllOrder(clientId);
                if (result is not  null)
                    return Ok(result);
                return BadRequest();
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
                var result = await _clientService.GetOrder(orderId);
                if (result is not null)
                    return Ok(result);
                return BadRequest();
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
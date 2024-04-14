using Application.Models.DTOs.CategoryDTOs;
using Application.Services.IUserServices;
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


        [HttpPost("createCategory")]
        public async Task<ActionResult<bool>> CreateCategory(CreateCategoryDto dto)
        {
            try
            {
                var result = await _storeService.CreateCategory(dto);
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

    }
}

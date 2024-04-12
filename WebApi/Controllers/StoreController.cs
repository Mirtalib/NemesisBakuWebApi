using Application.Models.DTOs.ShoesDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        public StoreController() { }

        [HttpPost("Add")]
        public ActionResult<AddShoeDto> Index([FromForm] AddShoeDto addShoeDto)
        {
            return Ok(addShoeDto);
        }
    }
}

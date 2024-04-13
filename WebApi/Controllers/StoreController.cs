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
        public ActionResult Index([FromForm]AddShoeDto dto)
        {
            var key = new KeyValuePair<byte, byte>();
             
            DateTime date = DateTime.Now.AddDays(-7);
            if (date.Date < DateTime.Now)
            {
                return Ok(dto.ShoeCountSize);
            }
            return BadRequest("isdeme3di ay gijdillaq");
        }
    }
}

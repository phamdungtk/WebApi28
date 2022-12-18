using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi28.Models;

namespace WebApi28.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SildeController : ControllerBase
    {
        private WebApi28Context db = new WebApi28Context();
        [Route("Get-Sile-All")]
        [HttpGet]
        public IActionResult getall()
        {
            try
            {
                var result = db.Sildes.ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("Err");
            }
        }
    }
}

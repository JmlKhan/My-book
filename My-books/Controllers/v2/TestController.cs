using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_books.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-teset-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController v2");
        }
    }
}

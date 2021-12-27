﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace My_books.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController v1");
        }
    }
}
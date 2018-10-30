using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartLock.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocksController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetLock(int id)
        {
            return Ok();
        }
    }
}

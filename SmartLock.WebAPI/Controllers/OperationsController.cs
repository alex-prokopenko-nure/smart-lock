using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartLock.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetLockOperations(int id)
        {
            return Ok();
        }

        [HttpPost("{lockId}/opened")]
        public IActionResult Opened(int lockId)
        {
            return Ok();
        }

        [HttpPost("{lockId}/closed")]
        public IActionResult Closed(int lockId)
        {
            return Ok();
        }

        [HttpPost("{lockId}/failed")]
        public IActionResult Failed(int lockId)
        {
            return Ok();
        }
    }
}
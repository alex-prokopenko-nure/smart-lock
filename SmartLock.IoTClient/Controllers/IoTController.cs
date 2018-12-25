using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartLock.IoTClient.Controllers
{
    [Route("api/iot")]
    public class IoTController : ControllerBase
    {
        [HttpPost("{id}/open")]
        public async Task<IActionResult> Open(int id)
        {
            Program._serialPort.Write("1");
            return Ok();
        }

        [HttpPost("{id}/close")]
        public async Task<IActionResult> Close(int id)
        {
            Program._serialPort.Write("0");
            return Ok();
        }
    }
}

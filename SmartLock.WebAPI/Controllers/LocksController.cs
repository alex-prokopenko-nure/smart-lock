using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using SmartLock.WebAPI.Services.Interfaces;

namespace SmartLock.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocksController : ControllerBase
    {
        private readonly ILocksService _locksService;

        public LocksController(ILocksService locksService)
        {
            _locksService = locksService;
        }

        #region Get
        [HttpGet("{id}")]
        public async Task<ActionResult<Lock>> GetLock(int id)
        {
            var result = await _locksService.GetLock(id);
            return Ok(result);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<Lock>> CreateLock()
        {
            var result = await _locksService.CreateLock();
            return Ok(result);
        }

        [HttpPost("{lockId}/opened")]
        public async Task<IActionResult> Opened(int lockId)
        {
            await _locksService.LockOpened(lockId);
            return Ok();
        }

        [HttpPost("{lockId}/closed")]
        public async Task<IActionResult> Closed(int lockId)
        {
            await _locksService.LockClosed(lockId);
            return Ok();
        }

        [HttpPost("{lockId}/failed")]
        public async Task<IActionResult> Failed(int lockId)
        {
            await _locksService.LockFailed(lockId);
            return Ok();
        }
        #endregion

        #region Put
        #endregion

        #region Delete
        #endregion
    }
}

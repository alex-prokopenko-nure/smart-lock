using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SmartLock.WebAPI.Hubs;
using SmartLock.WebAPI.Hubs.Interfaces;
using SmartLock.WebAPI.Services.Interfaces;
using SmartLock.WebAPI.ViewModels;

namespace SmartLock.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocksController : ControllerBase
    {
        private readonly ILocksService _locksService;
        private readonly IUsersService _usersService;
        private readonly IHubContext<LockHub, ILockHub> _hubContext;

        public LocksController(
            ILocksService locksService, 
            IUsersService usersService, 
            IHubContext<LockHub, ILockHub> hubContext
            )
        {
            _locksService = locksService;
            _usersService = usersService;
            _hubContext = hubContext;
        }

        #region Get

        /// <summary>
        /// Retrieve the Lock by its ID.
        /// </summary>
        /// <param name="id">The ID of the desired Lock</param>
        /// <returns>Queried Lock</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Lock>> GetLock(int id)
        {
            var result = await _locksService.GetLock(id);
            return Ok(result);
        }

        [HttpGet("all-locks/{userId}")]
        public async Task<ActionResult<IEnumerable<LockRent>>> GetAllUsersLocks(int userId)
        {
            var result = await _locksService.GetAllUsersLocks(userId);
            return Ok(result);
        }

        [HttpGet("{lockId}/renters")]
        public async Task<ActionResult<IEnumerable<LockRent>>> GetAllRenters(int lockId, RentRights rights)
        {
            var result = await _locksService.GetAllRenters(lockId, rights);
            return Ok(result);
        }

        [HttpGet("{lockId}/operations")]
        public async Task<ActionResult<IEnumerable<LockOperation>>> GetLockOperations(int lockId, int userId)
        {
            var result = await _locksService.GetOperations(lockId, userId);
            return Ok(result);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> CreateLock()
        {
            await _locksService.CreateLock();
            return Ok();
        }

        [HttpPost("{lockId}/opened")]
        public async Task<IActionResult> Opened(int lockId)
        {
            await _locksService.LockOpened(lockId);
            await _hubContext.Clients.All.SetLockState(lockId, false);
            return Ok();
        }

        [HttpPost("{lockId}/closed")]
        public async Task<IActionResult> Closed(int lockId)
        {
            await _locksService.LockClosed(lockId);
            await _hubContext.Clients.All.SetLockState(lockId, true);
            return Ok();
        }

        [HttpPost("{lockId}/failed")]
        public async Task<IActionResult> Failed(int lockId)
        {
            await _locksService.LockFailed(lockId);
            return Ok();
        }

        [HttpPost("share-rights")]
        public async Task<IActionResult> ShareRights(ShareRightsViewModel shareRightsViewModel)
        {
            await _locksService.ShareRights(shareRightsViewModel);
            return Ok();
        }
        #endregion

        #region Put
        [HttpPut("{lockId}")]
        public async Task<IActionResult> EditLock(int lockId, Lock lockModel)
        {
            await _locksService.EditLock(lockId, lockModel);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{lockId}")]
        public async Task<IActionResult> DeleteLock(int lockId)
        {
            await _locksService.DeleteLock(lockId);
            return Ok();
        }

        [HttpDelete("{lockId}/cancel/{userId}")]
        public async Task<IActionResult> CancelRights(int lockId, int userId)
        {
            await _locksService.CancelRights(lockId, userId);
            return Ok();
        }
        #endregion
    }
}

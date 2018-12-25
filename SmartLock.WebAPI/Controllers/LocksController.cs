using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
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
        /// Returns the Lock by its ID.
        /// </summary>
        /// <param name="id">The ID of the desired Lock</param>
        /// <returns>Queried Lock</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Lock>> GetLock(int id)
        {
            var result = await _locksService.GetLock(id);
            return Ok(result);
        }

        /// <summary>
        /// Returns all Locks rented by given user.
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <returns>All locks for user</returns>
        [Authorize]
        [HttpGet("all-locks/{userId}")]
        public async Task<ActionResult<IEnumerable<LockRent>>> GetAllUsersLocks(int userId)
        {
            var result = await _locksService.GetAllUsersLocks(userId);
            return Ok(result);
        }

        /// <summary>
        /// Returns all renters for given lock rented with some rights.
        /// </summary>
        /// <param name="lockId">The ID of the lock</param>
        /// <param name="rights">Rights on the rented lock</param>
        /// <returns>All renters for lock</returns>
        [Authorize]
        [HttpGet("{lockId}/renters")]
        public async Task<ActionResult<IEnumerable<LockRent>>> GetAllRenters(int lockId, RentRights rights)
        {
            var result = await _locksService.GetAllRenters(lockId, rights);
            return Ok(result);
        }

        /// <summary>
        /// Returns all operations for user on the rent period.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <param name="userId">The ID of the desired User</param>
        /// <returns>Queried Lock;s operations</returns>
        [Authorize]
        [HttpGet("{lockId}/operations")]
        public async Task<ActionResult<IEnumerable<LockOperation>>> GetLockOperations(int lockId, int userId)
        {
            var result = await _locksService.GetOperations(lockId, userId);
            return Ok(result);
        }
        #endregion

        #region Post
        /// <summary>
        /// Creates new lock in the system.
        /// </summary>
        /// <returns>OK</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLock()
        {
            await _locksService.CreateLock();
            return Ok();
        }

        /// <summary>
        /// Opens Lock.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <returns>OK</returns>
        [HttpPost("{lockId}/open")]
        public async Task<IActionResult> Open(int lockId)
        {
            await _locksService.OpenLock(lockId);
            return Ok();
        }

        /// <summary>
        /// Closes Lock.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <returns>OK</returns>
        [HttpPost("{lockId}/close")]
        public async Task<IActionResult> Close(int lockId)
        {
            await _locksService.CloseLock(lockId);
            return Ok();
        }

        /// <summary>
        /// Notifies system about Lock opening.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <returns>OK</returns>
        [HttpPost("{lockId}/opened")]
        public async Task<IActionResult> Opened(int lockId)
        {
            await _locksService.LockOpened(lockId);
            await _hubContext.Clients.All.SetLockState(lockId, false);
            return Ok();
        }

        /// <summary>
        /// Notifies system about Lock closing.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <returns>OK</returns>
        [HttpPost("{lockId}/closed")]
        public async Task<IActionResult> Closed(int lockId)
        {
            await _locksService.LockClosed(lockId);
            await _hubContext.Clients.All.SetLockState(lockId, true);
            return Ok();
        }

        /// <summary>
        /// Notifies system about Lock opening fail.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <returns>OK</returns>
        [HttpPost("{lockId}/failed")]
        public async Task<IActionResult> Failed(int lockId)
        {
            await _locksService.LockFailed(lockId);
            return Ok();
        }

        /// <summary>
        /// Shares rights on the lock to another user.
        /// </summary>
        /// <param name="shareRightsViewModel">View model of sharing query</param>
        /// <returns>OK</returns>
        [Authorize]
        [HttpPost("share-rights")]
        public async Task<IActionResult> ShareRights(ShareRightsViewModel shareRightsViewModel)
        {
            await _locksService.ShareRights(shareRightsViewModel);
            return Ok();
        }
        #endregion

        #region Put
        /// <summary>
        /// Edits the Lock by its ID.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <param name="lockModel">The model to be applied</param>
        /// <returns>OK</returns>
        [Authorize]
        [HttpPut("{lockId}")]
        public async Task<IActionResult> EditLock(int lockId, Lock lockModel)
        {
            await _locksService.EditLock(lockId, lockModel);
            return Ok();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Deletes the Lock by its ID.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <returns>OK</returns>
        [Authorize]
        [HttpDelete("{lockId}")]
        public async Task<IActionResult> DeleteLock(int lockId)
        {
            await _locksService.DeleteLock(lockId);
            return Ok();
        }

        /// <summary>
        /// Cancels lock rent by lockId and userId.
        /// </summary>
        /// <param name="lockId">The ID of the desired Lock</param>
        /// <param name="userId">The ID of the desired User</param>
        /// <returns>OK</returns>
        [Authorize]
        [HttpDelete("{lockId}/cancel/{userId}")]
        public async Task<IActionResult> CancelRights(int lockId, int userId)
        {
            await _locksService.CancelRights(lockId, userId);
            return Ok();
        }
        #endregion
    }
}

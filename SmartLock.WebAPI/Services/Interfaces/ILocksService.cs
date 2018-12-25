using Domain.Common.Enums;
using Domain.Models;
using SmartLock.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services.Interfaces
{
    public interface ILocksService
    {
        Task<Lock> GetLock(int id);
        Task<IEnumerable<LockRent>> GetAllUsersLocks(int userId);
        Task<IEnumerable<LockRent>> GetAllRenters(int lockId, RentRights rights);
        Task CreateLock();
        Task OpenLock(int id);
        Task CloseLock(int id);
        Task LockClosed(int id);
        Task LockOpened(int id);
        Task LockFailed(int id);
        Task<IEnumerable<LockOperation>> GetOperations(int lockId, int userId);
        Task ShareRights(ShareRightsViewModel shareRightsViewModel);
        Task<Lock> EditLock(int lockId, Lock lockModel);
        Task DeleteLock(int lockId);
        Task CancelRights(int lockId, int userId);
    }
}

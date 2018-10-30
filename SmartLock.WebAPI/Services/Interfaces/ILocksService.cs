using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services.Interfaces
{
    public interface ILocksService
    {
        Task<Lock> GetLock(int id);
        Task<Lock> CreateLock();
        Task LockClosed(int id);
        Task LockOpened(int id);
        Task LockFailed(int id);
    }
}

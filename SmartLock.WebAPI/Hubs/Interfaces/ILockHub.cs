using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Hubs.Interfaces
{
    public interface ILockHub
    {
        Task SetLockState(int lockId, bool locked);
    }
}

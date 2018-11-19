using Microsoft.AspNetCore.SignalR;
using SmartLock.WebAPI.Hubs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Hubs
{
    public class LockHub: Hub<ILockHub>
    {
        public LockHub()
        {
        }

        public async Task SetLockState(int lockId, bool locked)
        {
            await Clients.All.SetLockState(lockId, locked);
        }
    }
}

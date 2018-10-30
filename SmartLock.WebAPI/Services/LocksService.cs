using Domain.Common.Enums;
using Domain.Contexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SmartLock.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services
{
    public class LocksService : ILocksService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LocksService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Lock> GetLock(int id)
        {
            var result = await _applicationDbContext.Locks.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Lock> CreateLock()
        {
            Lock lockEntity = GenerateLock();
            await _applicationDbContext.Locks.AddAsync(lockEntity);
            await _applicationDbContext.SaveChangesAsync();
            return lockEntity;
        }

        public async Task LockClosed(int id)
        {
            Lock currLock = await GetLock(id);
            currLock.Locked = true;
            LockOperation operation = new LockOperation
            {
                Lock = currLock,
                LockId = id,
                CreateTime = DateTime.Now,
                State = LockState.Closed
            };
            await _applicationDbContext.LockOperations.AddAsync(operation);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task LockFailed(int id)
        {
            Lock currLock = await GetLock(id);
            LockOperation operation = new LockOperation
            {
                Lock = currLock,
                LockId = id,
                CreateTime = DateTime.Now,
                State = LockState.Failed
            };
            await _applicationDbContext.LockOperations.AddAsync(operation);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task LockOpened(int id)
        {
            Lock currLock = await GetLock(id);
            currLock.Locked = false;
            LockOperation operation = new LockOperation
            {
                Lock = currLock,
                LockId = id,
                CreateTime = DateTime.Now,
                State = LockState.Opened
            };
            await _applicationDbContext.LockOperations.AddAsync(operation);
            await _applicationDbContext.SaveChangesAsync();
        }

        private Lock GenerateLock()
        {
            Random random = new Random();
            Lock entity = new Lock
            {
                Password = random.Next(1000, 9999).ToString(),
                Locked = false
            };
            return entity;
        }
    }
}

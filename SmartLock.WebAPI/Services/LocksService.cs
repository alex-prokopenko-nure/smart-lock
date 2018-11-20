using Domain.Common.Enums;
using Domain.Contexts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using SmartLock.WebAPI.Services.Interfaces;
using SmartLock.WebAPI.ViewModels;
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

        public async Task<IEnumerable<LockRent>> GetAllUsersLocks(int userId)
        {
            DateTime dateTimeNow = DateTime.Now;
            var result = await _applicationDbContext.LockRents
                .Where(x => x.UserId == userId && CheckTiming(x))
                .Include(x => x.Lock)
                .ToListAsync();

            foreach(var res in result)
            {
                res.Lock.LockRents = null;
            }

            return result;
        }

        public async Task CreateLock()
        {
            Lock lockEntity = GenerateLock();
            await _applicationDbContext.Locks.AddAsync(lockEntity);
            await _applicationDbContext.SaveChangesAsync();
            List<User> admins = await _applicationDbContext.Users.Where(x => x.Role == ApplicationRole.Admin).ToListAsync();
            List<LockRent> adminRents = new List<LockRent>();
            foreach (var admin in admins)
            {
                LockRent lockRent = 
                    new LockRent {
                        LockId = lockEntity.Id,
                        Lock = lockEntity,
                        RentStart = DateTime.Now,
                        Rights = RentRights.Admin,
                        UserId = admin.Id,
                        User = admin
                    };
                adminRents.Add(lockRent);
            }
            await _applicationDbContext.LockRents.AddRangeAsync(adminRents);
            await _applicationDbContext.SaveChangesAsync();
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

        public async Task<IEnumerable<LockOperation>> GetOperations(int lockId, int userId)
        {
            var currRent = await GetCurrentRent(lockId, userId);
            if (currRent == null)
            {
                return null;
            }
            var lockOperations = _applicationDbContext.LockOperations.Where(x => x.LockId == lockId);
            return lockOperations.Where(x => x.CreateTime >= currRent.RentStart && (!currRent.RentFinish.HasValue || x.CreateTime <= currRent.RentFinish)).OrderByDescending(x => x.CreateTime);
        }

        public async Task ShareRights(ShareRightsViewModel shareRightsViewModel)
        {
            var currRent = await GetCurrentRent(shareRightsViewModel.LockId, shareRightsViewModel.OwnerId);
            if (currRent != null)
            {
                return;
            }
            LockRent ownerRent =
                new LockRent
                {
                    UserId = shareRightsViewModel.OwnerId,
                    LockId = shareRightsViewModel.LockId,
                    Rights = shareRightsViewModel.Rights,
                    RentStart = shareRightsViewModel.From,
                    RentFinish = shareRightsViewModel.To
                };
            await _applicationDbContext.LockRents.AddAsync(ownerRent);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Lock> EditLock(int lockId, Lock lockModel)
        {
            var lockToEdit = await GetLock(lockId);
            lockToEdit.Info = lockModel.Info;
            _applicationDbContext.Locks.Update(lockToEdit);
            await _applicationDbContext.SaveChangesAsync();
            return lockToEdit;
        }

        public async Task DeleteLock(int lockId)
        {
            Lock lockToDelete = new Lock { Id = lockId };
            _applicationDbContext.Attach(lockToDelete);
            _applicationDbContext.Remove(lockToDelete);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task CancelRights(int lockId, int userId)
        {
            LockRent rent = await _applicationDbContext.LockRents
                .FirstOrDefaultAsync(x => x.LockId == lockId && x.UserId == userId && CheckTiming(x));
            rent.RentFinish = DateTime.Now;
            _applicationDbContext.LockRents.Update(rent);
            await _applicationDbContext.SaveChangesAsync();
        }

        private bool CheckTiming(LockRent rent)
        {
            DateTime dateTimeNow = DateTime.Now;
            return rent.RentStart <= dateTimeNow && (!rent.RentFinish.HasValue || rent.RentFinish > dateTimeNow);
        }

        private async Task<LockRent> GetCurrentRent(int lockId, int userId)
        {
            return await _applicationDbContext.LockRents
            .FirstOrDefaultAsync(x => x.LockId == lockId && x.UserId == userId && CheckTiming(x) &&
                x.RentStart == _applicationDbContext.LockRents
                .Where(z => z.UserId == userId && z.LockId == lockId)
                .Max(y => y.RentStart));
        }

        public async Task<IEnumerable<LockRent>> GetAllRenters(int lockId, RentRights rights)
        {
            var renters = await _applicationDbContext.LockRents
                .Where(x => x.LockId == lockId && CheckTiming(x) && (int)x.Rights > (int)rights)
                .Include(x => x.User)
                .ToListAsync();

            foreach (var rent in renters)
            {
                rent.User.LockRents = null;
            }

            return renters;
        }
    }
}

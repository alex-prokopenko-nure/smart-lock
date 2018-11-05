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
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<User> GetUser(int userId)
        {
            var userToReturn = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return userToReturn;
        }
    }
}

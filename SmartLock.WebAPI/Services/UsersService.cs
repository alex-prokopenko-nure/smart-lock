using Domain.Common.Enums;
using Domain.Common.Helpers;
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
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UsersService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _applicationDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            var userToReturn = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return userToReturn;
        }

        public async Task<int> LoginUser(LoginViewModel model)
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email && 
                    AccountHelper.GetPasswordHash(model.Password) == x.Password);
            if (user == null)
            {
                throw new Exception();
            }
            return user.Id;
        }

        public async Task RegisterUser(RegisterViewModel model)
        {
            var duplicate = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == model.Email || x.Username == model.Username);
            if (duplicate != null)
            {
                throw new Exception();
            }
            User user = new User
            {
                Email = model.Email,
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = AccountHelper.GetPasswordHash(model.Password)
            };
            await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}

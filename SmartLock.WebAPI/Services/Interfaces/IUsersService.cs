using Domain.Models;
using SmartLock.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int userId);
        Task<int> LoginUser(LoginViewModel model);
        Task RegisterUser(RegisterViewModel model);
    }
}

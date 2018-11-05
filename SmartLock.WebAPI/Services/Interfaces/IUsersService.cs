using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetUser(int userId);
    }
}

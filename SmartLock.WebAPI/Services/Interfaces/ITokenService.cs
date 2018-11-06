using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.WebAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(int userId);
        int ParseJwtToken(string jwtToken);
    }
}

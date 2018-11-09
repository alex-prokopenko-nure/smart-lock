using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLock.WebAPI.Services.Interfaces;
using SmartLock.WebAPI.ViewModels;

namespace SmartLock.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;

        public UsersController(IUsersService usersService, ITokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginViewModel model)
        {
            try
            {
                int userId = await _usersService.LoginUser(model);
                string token = _tokenService.BuildToken(userId);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                await _usersService.RegisterUser(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("{userId}/info")]
        public async Task<ActionResult<User>> GetUserInfo(int userId)
        {
            try
            {
                var user = await _usersService.GetUser(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
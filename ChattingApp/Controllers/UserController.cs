using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Contracts;
using ChattingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChattingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepo _repo;
        private readonly IRepoBase<User> _repoBase;

        public UserController(ILogger<UserController> logger, IUserRepo repo, IRepoBase<User> repoBase)
        {
            _logger = logger;
            _repo = repo;
            _repoBase = repoBase;
        }

        [HttpGet("roomId")]
        public async Task<IActionResult> GetAllUsers(int roomId)
        {
            var users = await _repo.GetAllUserByRoomId(roomId);
            // handle error 400 respose
            if (users is null) return BadRequest();
            return Ok(users);
        }

        [HttpGet("userId")]
        public async Task<IActionResult> GetUserByRoom(int userId)
        {
            var user = await _repo.GetUserAsync(userId);
            // handle error 404 respose
            if (user is null) return NotFound(404);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(User user)
        {
            /// handle error
            if (user is null) return BadRequest(400);
            _repo.CreateUserAsync(user);
            await _repoBase.Save();
            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            _repo.DeleteUser(userId);
            await _repoBase.Save();
            return Ok(201);
        }
    }
}
using System;
using ChattingApp.Contracts;
using ChattingApp.Error_Model;
using ChattingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChattingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IRoomRepo _roomRepo;
        private readonly IRepoBase<User> _repoBase;

        public UserController(
            IUserRepo repo, 
            IRepoBase<User> repoBase, 
            IRoomRepo roomRepo)
        {
            _repo = repo;
            _repoBase = repoBase;
            _roomRepo=roomRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int roomId)
        {
            var room = await _roomRepo.GetRoomAsync(roomId);
            // handle error 404
            if (room is null) return NotFound(new ApiReposnse(404));
            var users = await _repo.GetAllUserByRoomId(roomId);
            // handle error 400 respose
            if (users is null) return BadRequest(new ApiReposnse(400));
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByRoom(int userId, int roomId)
        {
            var room = await _roomRepo.GetRoomAsync(roomId);
            // handle error 404
            if (room is null) return NotFound(new ApiReposnse(404));
            var user = await _repo.GetUserAsync(userId);
            // handle error 404 respose
            if (user is null) return NotFound(new ApiReposnse(404));
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody]User user)
        {
            /// handle error
            if (user is null) return BadRequest(new ApiReposnse(400));
            _repo.CreateUserAsync(user);    
            await _repoBase.Save();
            return CreatedAtAction("GetUserByRoom", new { Id = user.Id}, user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            _repo.DeleteUser(userId);
            await _repoBase.Save();
            return NoContent();
        }
    }
}
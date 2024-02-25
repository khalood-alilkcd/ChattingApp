using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Contracts;
using ChattingApp.Error_Model;
using ChattingApp.Models;
using ChattingApp.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChattingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomRepo _roomRepo;
        private readonly IRepoBase<Room> _repo;

        public RoomController(ILogger<RoomController> logger, IRoomRepo roomRepo, IRepoBase<Room> repo)
        {
            _logger = logger;
            _roomRepo = roomRepo;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoomAsync()
        {
            var rooms = await _roomRepo.GetAllRoomAsync();
            return Ok(rooms);
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomAsync(int roomId)
        {
            var room = await _roomRepo.GetRoomAsync(roomId);
            if (room == null) return NotFound(new ApiReposnse(404));
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromBody] Room room)
        {
            if (room is null) return BadRequest(new ApiReposnse(400));
            _roomRepo.CreateRoom(room);
            await _repo.Save();
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRoomAsync(int roomId)
        {
            var room = await _roomRepo.GetRoomAsync(roomId);
            if (room == null) return NotFound(new ApiReposnse(404));
            _roomRepo.DeleteRoom(roomId);
            await _repo.Save();
            return NoContent();
        }
    }
}
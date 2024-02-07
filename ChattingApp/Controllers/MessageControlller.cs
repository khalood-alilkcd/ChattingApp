using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChattingApp.Contracts;
using ChattingApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChattingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageControlller : ControllerBase
    {
        private readonly ILogger<MessageControlller> _logger;
        private readonly IMessageRepo _messageRepo;
        private readonly IRepoBase<Message> _repo;

        public MessageControlller(
            ILogger<MessageControlller> logger,
            IMessageRepo messageRepo,
            IRepoBase<Message> repo
            )
        {
            _logger = logger;
            _messageRepo = messageRepo;
            _repo = repo;
        }

        [HttpGet("convId")]
        public async Task<IActionResult> GetAllMassageAsync(int convId)
        {
            var msgs = await _messageRepo.GetAllMessageAsync(convId);
            if (msgs is null) return BadRequest(400);
            return Ok(msgs);
        }

        [HttpGet("convId, userId")]
        public async Task<IActionResult> GetMessageAsync(int convId, int userId)
        {
            var msg = await _messageRepo.GetMessageAsync(convId, userId);
            // handle error
            if (msg is null) return NotFound();
            return Ok(msg);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message msg)
        {
            _messageRepo.CreateMessage(msg);
            await _repo.Save();
            return Ok(201);
        }
    }
}